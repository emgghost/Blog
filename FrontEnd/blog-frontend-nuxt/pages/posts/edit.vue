<template>
  <v-container>
    <v-form @submit.prevent="updatePost">
      <v-text-field v-model="post.title" label="عنوان"></v-text-field>
      <RichTextEditor v-model="post.content" />
      <v-btn type="submit" color="primary">ذخیره تغییرات</v-btn>
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
        content: ''
      }
    }
  },
  async fetch() {
    const slug = this.$route.params.slug
    this.post = await this.$axios.$get(`/api/blog/posts/${slug}`)
  },
  methods: {
    async updatePost() {
      await this.$axios.$put(`/api/blog/posts/${this.post.slug}`, this.post)
      this.$router.push(`/posts/${this.post.slug}`)
    }
  }
}
</script>
