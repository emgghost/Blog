<template>
  <div class="container mx-auto px-4 py-8">
    <div v-if="tag" class="mb-8">
      <h1 class="text-3xl font-bold mb-4">Posts tagged with "{{ tag.name }}"</h1>
      <p class="text-gray-600 mb-6">{{ tag.description }}</p>
    </div>

    <div v-if="loading" class="flex justify-center items-center">
      <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-gray-900"></div>
    </div>

    <div v-else-if="error" class="text-center py-8">
      <p class="text-red-500">Error loading posts: {{ error.message }}</p>
    </div>

    <div v-else-if="posts.length === 0" class="text-center py-8">
      <p class="text-gray-600">No posts found with this tag.</p>
    </div>

    <v-row v-else>
      <v-col v-for="post in posts" :key="post.id" cols="12" md="6">
        <v-card class="elevation-3 blog-card">
          <v-img v-if="post.imageUrl" :src="api.fileUrl + post.imageUrl" height="200px" cover class="rounded-t-lg"></v-img>
          <v-card-title class="text-primary font-weight-bold text-truncate">
            {{ post.title }}
          </v-card-title>
          <v-card-subtitle class="text-grey-darken-1">
            🗓 {{ new Date(post.createdAt).toLocaleDateString("fa-IR") }}
          </v-card-subtitle>
          <v-card-text class="text-truncate">
            {{ post.description }}
          </v-card-text>
          <v-card-actions>
            <v-btn color="primary" :to="`/posts/${post.slug}`" variant="flat" class="w-100">
              مشاهد
              <v-icon end>mdi-chevron-left</v-icon>
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script setup>
import {useApi} from "../../useApi";

const route = useRoute()
const api = useApi()

const slug = route.params.slug
const { data: tagPosts, pending: loading, error } = useAsyncData(
    `posts-tag-${slug}`,
    () => api.getPostByTag(slug)
)

const tag = computed(() => tagPosts.value?.data || null)
const posts = computed(() => tagPosts.value?.data?.blogPosts || [])

const formatDate = (date) => {
  return new Date(date).toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  })
}
</script>