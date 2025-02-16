<template>
  <v-container>
    <v-form @submit.prevent="updatePost">
      <v-img :src="api.fileUrl + post.imageUrl" width="100%" class="mb-4"></v-img>
      <v-file-input
        label="عکس پست"
        @change="uploadImage"
        accept="image/*"
      ></v-file-input>
      <v-btn @click="deleteImage" color="error">حذف تصویر</v-btn>
      <v-text-field v-model="post.title" label="عنوان"></v-text-field>
      <v-select
        v-model="post.categoryIds"
        :items="categories"
        item-title="name"
        item-value="id"
        label="دسته‌بندی‌ها"
        multiple
        chips
        :loading="isLoadingCategories"
        :disabled="isSubmitting || isLoadingCategories"
      ></v-select>
      <v-select
        v-model="post.tagIds"
        :items="tags"
        item-title="name"
        item-value="id"
        label="برچسب‌ها"
        multiple
        chips
        :loading="isLoadingTags"
        :disabled="isSubmitting || isLoadingTags"
      ></v-select>
      <RichTextEditor v-model="post.content" />
      <v-btn type="submit" color="primary">ذخیره تغییرات</v-btn>
    </v-form>
  </v-container>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useApi } from "../../../useApi";
import RichTextEditor from "@/components/RichTextEditor.vue";

definePageMeta({
  layout: 'admin',
  middleware: ['auth']
})

const api = useApi();
const route = useRoute();
const router = useRouter();

const post = ref({
  id: 0,
  imageUrl: "",
  title: "",
  content: "",
  slug: "",
  categoryIds: [],
  tagIds: []
});
const categories = ref([])
const tags = ref([])
const isLoadingCategories = ref(false)
const isLoadingTags = ref(false)

const uploadImage = async (event) => {
  const file = event?.target?.files?.[0]
      if (!file) return

      isUploading.value = true
      const formData = new FormData()
      formData.append('file', file)

      try {
        const response = await api.uploadFile(formData)
        post.value.imageUrl = response.imageUrl
      } catch (error) {
        console.error('خطا در آپلود عکس:', error)
      } finally {
        isUploading.value = false
      }

};

const deleteImage = async () => {
  try {

    post.value.imageUrl = "";
  } catch (error){
    console.error("Error deleting image:", error);
  }
}

// Fetch post data when component is mounted
onMounted(async () => {
  try {
    await Promise.all([fetchCategories(), fetchTags()]);
    
    const { data } = await api.getPostBySlug(route.query.slug);
    if (data.value) {
      post.value = { 
        ...data.value,
        categoryIds: data.value.categories?.map(c => c.id) || [],
        tagIds: data.value.tags?.map(t => t.id) || []
      };
    }
  } catch (error) {
    console.error("Error fetching post:", error);
  }
});

// Update post
const updatePost = async () => {
  try {
    await api.updatePost(post.value.id , post.value);
    router.push(`/posts/${post.value.slug}`);
  } catch (error) {
    console.error("Error updating post:", error);
  }
};

    // Fetch categories and tags
    const fetchCategories = async () => {
      isLoadingCategories.value = true
      try {
        const { data } = await api.getCategories()
        categories.value = data.value
      } catch (error) {
        console.error('خطا در دریافت دسته‌بندی‌ها:', error)
      } finally {
        isLoadingCategories.value = false
      }
    }

    const fetchTags = async () => {
      isLoadingTags.value = true
      try {
        const { data } = await api.getTags()
        tags.value = data.value
      } catch (error) {
        console.error('خطا در دریافت برچسب‌ها:', error)
      } finally {
        isLoadingTags.value = false
      }
    }
</script>
