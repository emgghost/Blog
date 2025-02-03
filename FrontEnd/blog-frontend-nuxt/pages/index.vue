<template>
  <v-container>
    <h1 class="text-h4 mb-4">وبلاگ</h1>
    <v-progress-linear
      v-if="$fetchState.pending"
      indeterminate
      color="primary"
    ></v-progress-linear>
    <v-row v-else>
      <v-col
        v-for="post in posts"
        :key="post.id"
        cols="12"
        md="6"
      >
        <v-card>
          <v-card-title>{{ post.title }}</v-card-title>
          <v-card-actions>
            <v-btn
              color="primary"
              :to="`/posts/${post.slug}`"
            >
              مشاهده بیشتر
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      posts: []
    }
  },
  async fetch() {
    this.posts = await this.$axios.$get('/posts')
  }
}
</script>
