<template>
  <v-container class="border rounded-lg">
    <v-form @submit.prevent="updatePost">
      <div class="w-full mb-2 border rounded-xl p-2">
        <v-img :src="api.fileUrl + post.imageUrl" width="100%" class="mb-4"></v-img>
        <span class="w-full mb-2">ابعاد پیشنهادی عرض 600 با ارتفاع 200</span>
        <v-file-input
            label="عکس پست"
            @change="uploadImage"
            accept="image/*"
        ></v-file-input>
        <v-btn @click="deleteImage" color="error">حذف تصویر</v-btn>
      </div>
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
      <div class="w-full mb-2 border rounded-xl p-2">
        <q-checkbox v-model="post.addToSlider" label="استفاده در اسلایدر"/>
        <span class="p-2">(ابعاد پیشنهادی عرض 1500 با ارتفاع 500)</span>
        <v-img :src="api.fileUrl + post.sliderImageUrl" width="100%" class="mb-4"></v-img>
        <v-file-input
            v-if="!!post.addToSlider"
            label="عکس اسلایدر"
            @change="uploadSliderImage"
            accept="image/*"
        ></v-file-input>
        <v-btn v-if="!!post.addToSlider" @click="deleteSliderImage" color="error">حذف تصویر
        </v-btn>
      </div>
      <RichTextEditor v-model="post.content"/>
      <v-btn type="submit" class="mt-4" color="primary">ذخیره تغییرات</v-btn>
    </v-form>
  </v-container>
</template>

<script setup>
import {ref, onMounted} from "vue";
import {useRoute, useRouter} from "vue-router";
import {useApi} from "../../../useApi";
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
  sliderImageUrl: "",
  title: "",
  content: "",
  slug: "",
  addToSlider: false,
  categoryIds: [],
  tagIds: []
});
const categories = ref([])
const tags = ref([])

// Loading states
const isUploading = ref(false)
const isSubmitting = ref(false)
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
const uploadSliderImage = async (event) => {
  const file = event?.target?.files?.[0]
  if (!file) return

  isUploading.value = true
  const formData = new FormData()
  formData.append('file', file)

  try {
    const response = await api.uploadFile(formData)
    post.value.sliderImageUrl = response.imageUrl
  } catch (error) {
    console.error('خطا در آپلود عکس:', error)
  } finally {
    isUploading.value = false
  }

};

const deleteSliderImage = async () => {
  try {

    post.value.sliderImageUrl = "";
  } catch (error) {
    console.error("Error deleting image:", error);
  }
}
const deleteImage = async () => {
  try {

    post.value.imageUrl = "";
  } catch (error) {
    console.error("Error deleting image:", error);
  }
}

// Fetch post data when component is mounted
onMounted(async () => {
  try {
    await Promise.all([fetchCategories(), fetchTags()]);

    const {data} = await api.getPostBySlug(route.query.slug);
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
    await api.updatePost(post.value.id, post.value);
    router.push(`/posts/${post.value.slug}`);
  } catch (error) {
    console.error("Error updating post:", error);
  }
};

// Fetch categories and tags
const fetchCategories = async () => {
  isLoadingCategories.value = true
  try {
    const {data} = await api.getCategories()
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
    const {data} = await api.getTags()
    tags.value = data.value
  } catch (error) {
    console.error('خطا در دریافت برچسب‌ها:', error)
  } finally {
    isLoadingTags.value = false
  }
}
</script>
