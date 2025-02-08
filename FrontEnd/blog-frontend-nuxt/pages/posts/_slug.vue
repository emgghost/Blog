<template>
  <v-container>
    <v-progress-linear
      v-if="$fetchState.pending"
      indeterminate
      color="primary"
    ></v-progress-linear>
    <div v-else>
      <v-card>
        <v-img :src="(nuxtConfig.serverUrl + post.imageUrl)" width="100%" class="mb-4"></v-img>
        <v-card-title class="text-h3">{{ post.title }}</v-card-title>
        <v-card-text>
          <div class="text-body-1" v-html="post.content"></div>
          <v-chip
            v-for="category in post.categories"
            :key="category.id"
            class="ma-2"
            color="primary"
          >
            {{ category.name }}
          </v-chip>
        </v-card-text>
      </v-card>
    </div>
  </v-container>
</template>

<script>
import nuxtConfig from "@/nuxt.config";

export default {
  computed: {
    nuxtConfig() {
      return nuxtConfig
    }
  },
  data() {
    return {
      post: null
    }
  },
  async fetch() {
    const slug = this.$route.params.slug
    this.post = await this.$axios.$get(`/blog/BlogPosts/${slug}`)
  }
}
</script>
