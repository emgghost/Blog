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
          <v-img :src="(nuxtConfig.serverUrl + post.imageUrl)" height="200px"></v-img>
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
import nuxtConfig from "@/nuxt.config";
import api from "useApi";

export default {
  computed: {
    nuxtConfig() {
      return nuxtConfig
    }
  },
  data() {
    return {
      posts: []
    }
  },
  async fetch() {
    process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
    console.log("salam")
    this.posts = await api.getPosts()
  }
}
</script>
