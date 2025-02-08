<template>
  <v-container>
    <v-form @submit.prevent="createPost">
      <v-file-input
        label="عکس پست"
        @change="uploadImage"
        accept="image/*"
      ></v-file-input>
      <v-img v-if="post.imageUrl" :src="post.imageUrl" max-width="200"></v-img>
      <v-text-field v-model="post.title" label="عنوان"></v-text-field>
      <RichTextEditor v-model="post.content" />
    </v-form>
  </v-container>
</template>

<script>
import RichTextEditor from '@/components/RichTextEditor'

export default {
  components: {
    RichTextEditor
  },
  data() {
    return {
      post: {
        title: '',
        imageUrl: '',
        content: ''
      }
    }
  },
  methods: {
    async uploadImage(file) {
      const formData = new FormData()
      formData.append('file', file)

      try {
        const response = await this.$axios.$post('/FileManager/upload', formData, {
          headers: { 'Content-Type': 'multipart/form-data' }
        })
        this.post.imageUrl = response.imageUrl
      } catch (error) {
        console.error('خطا در آپلود عکس:', error)
      }
    },
    async createPost() {
      await this.$axios.$post('/api/blog/posts', {
        ...this.post,
        categoryIds: this.selectedCategories
      })
      this.$router.push('/')
    }
  }
}
</script>
