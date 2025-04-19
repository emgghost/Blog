<template>
  <v-container class="border rounded-lg">
    <v-form @submit.prevent="createPost">
      <div class="flex items-center">
      <v-img v-if="post.imageUrl" :src="fileUrl + post.imageUrl" class="mb-4" max-width="200" style="align-items: center;"></v-img>
      </div>
      <v-file-input
        label="عکس پست"
        variant="outlined"
        @change="uploadImage"
        accept="image/*"
        :loading="isUploading"
        :disabled="isUploading"
      ></v-file-input>
      <v-text-field 
        v-model="post.title" 
        label="عنوان"
        variant="outlined"
        :disabled="isSubmitting"
        required
      ></v-text-field>
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
