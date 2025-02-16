<template>
  <v-container class="blog-container">
    <!-- اسکلتی برای بارگذاری -->
    <v-skeleton-loader v-if="!status" type="image, article, button" class="w-100"></v-skeleton-loader>

    <v-card v-else class="elevation-3 blog-card">
      <v-img :src="fileUrl + post.imageUrl" class="blog-image"></v-img>

      <v-card-title class="text-h3 text-primary font-weight-bold text-center">
        {{ post.title }}
      </v-card-title>

      <v-card-text>
        <div class="text-body-1 blog-content" v-html="post.content"></div>

        <!-- نمایش دسته‌بندی‌ها -->
        <v-row class="mt-4">
          <v-col cols="12" class="text-center">
            <v-chip
              v-for="category in post.categories"
              :key="category.id"
              class="ma-2 blog-chip"
              color="primary"
              variant="flat"
            >
              {{ category.name }}
            </v-chip>
          </v-col>
        </v-row>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script setup>
import { useApi } from '../../useApi';
import { useRoute } from 'vue-router';

const router = useRoute();
const api = useApi();
const fileUrl = api.fileUrl;

// گرفتن اطلاعات پست با استفاده از API
const { data: post, status } = await api.getPostBySlug(router.params.slug);
</script>

<style scoped>
.blog-container {
  max-width: 900px;
  margin: auto;
  padding: 20px;
}

.blog-card {
  border-radius: 12px;
  overflow: hidden;
  transition: box-shadow 0.3s ease-in-out;
}

.blog-card:hover {
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.2);
}

.blog-image {
  height: 400px;
  object-fit: cover;
  border-radius: 12px 12px 0 0;
}

.blog-content {
  line-height: 1.8;
  color: #424242;
  text-align: justify;
}

.blog-chip {
  font-size: 14px;
  font-weight: bold;
  border-radius: 8px;
}
</style>
