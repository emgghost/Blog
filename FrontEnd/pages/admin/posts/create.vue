<template>
  <v-container class="border rounded-lg">
    <v-form @submit.prevent="createPost">
      <div class="flex items-center">
      <div class="w-full mb-2 border rounded-xl p-2">
        <v-img v-if="post.imageUrl" :src="fileUrl + post.imageUrl" width="100%" class="mb-4"></v-img>
        <span class="w-full mb-2">ابعاد پیشنهادی عرض 600 با ارتفاع 200</span>
        <v-file-input
            label="عکس پست"
            @change="uploadImage"
            accept="image/*"
            :loading="isUploading"
            :disabled="isUploading"
        ></v-file-input>
        <v-btn @click="deleteImage" color="error">حذف تصویر</v-btn>
      </div>
      <v-text-field 
        v-model="post.title" 
        label="عنوان"
        variant="outlined"
        :disabled="isSubmitting"
        required
      ></v-text-field>
      <div class="w-full grid grid-cols-1 lg:grid-cols-2 items-center gap-2">
        <v-select
            v-model="post.categoryIds"
            :items="categories"
            item-title="name"
            variant="outlined"
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
            variant="outlined"
            label="برچسب‌ها"
            multiple
            chips
            :loading="isLoadingTags"
            :disabled="isSubmitting || isLoadingTags"
        ></v-select>
      </div>
        <div class="w-full mb-2 border rounded-xl p-2">
          <q-checkbox v-model="post.addToSlider" label="استفاده در اسلایدر"/>
          <span class="p-2">(ابعاد پیشنهادی عرض 1500 با ارتفاع 500)</span>
          <v-img :src="fileUrl + post.sliderImageUrl" width="100%" class="mb-4"></v-img>
          <v-file-input
              v-if="!!post.addToSlider"
              label="عکس اسلایدر"
              @change="uploadSliderImage"
              accept="image/*"
          ></v-file-input>
          <v-btn v-if="!!post.addToSlider" @click="deleteSliderImage" color="error">حذف تصویر
          </v-btn>
        </div>
      </div>
      <RichTextEditor
        v-model="post.content"
      />
      <v-btn
        type="submit"
        color="primary"
        class="mt-4"
        :loading="isSubmitting"
        :disabled="!post.title || !post.content"
      >
        ایجاد پست
      </v-btn>
    </v-form>
  </v-container>
</template>

<script setup>
import RichTextEditor from '@/components/RichTextEditor.vue'
import { useApi } from '../../../useApi'
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'

definePageMeta({
  layout: 'admin',
  middleware: ['auth']
})

const api = useApi()
const router = useRouter()
const fileUrl = api.fileUrl

// Loading states
const isUploading = ref(false)
const isSubmitting = ref(false)
const isLoadingCategories = ref(false)
const isLoadingTags = ref(false)

// Data
const post = ref({
  title: '',
  content: '',
  imageUrl: '',
  categoryIds: [],
  addToSlider:false,
  tagIds: [],
  authorId: process.client ? localStorage.getItem('userId') : null
})
const categories = ref([])
const tags = ref([])

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
}
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
const createPost = async () => {
  if (!post.value.title || !post.value.content) return

  isSubmitting.value = true
  try {
    await api.createPost(post.value)
    router.push('/admin/posts')
  } catch (error) {
    console.error('خطا در ایجاد پست:', error)
  } finally {
    isSubmitting.value = false
  }
}

// Fetch data on component mount
onMounted(() => {
  fetchCategories()
  fetchTags()
})

</script>
