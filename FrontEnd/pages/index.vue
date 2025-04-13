<template>
  <v-container fluid class="blog-container">
    <h1 class="text-h4 mb-6 text-center font-weight-bold">📖 وبلاگ</h1>

    <!-- وضعیت بارگذاری -->
    <v-row v-if="!status">
      <v-col v-for="n in 4" :key="n" cols="12" sm="6" md="4">
        <v-skeleton-loader type="image, article, button"></v-skeleton-loader>
      </v-col>
    </v-row>

    <!-- نمایش خطا -->
    <v-alert v-else-if="error" type="error" variant="tonal" class="text-center">
      ❌ خطا در دریافت داده‌ها: {{ error.message }}
    </v-alert>

    <!-- نمایش لیست پست‌ها -->
    <div class="w-full grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4" v-else>
      <div v-for="post in posts" :key="post.id" class="col-span-1">
        <v-card class="elevation-3 blog-card d-flex w-full flex-column h-100">
          <v-img :src="fileUrl + post.imageUrl" height="200px" cover class="rounded-t-lg"></v-img>
          <v-card-title @click="goToPost(post.slug)" class="text-[#00524B] font-weight-bold text-truncate">
            {{ post.title }}
          </v-card-title>
          <v-card-subtitle class="text-grey-darken-1">
            🗓 {{ new Date(post.createdAt).toLocaleDateString("fa-IR") }}
          </v-card-subtitle>
          <v-card-text class="text-truncate">
            {{ post.description }}
          </v-card-text>
          <v-spacer></v-spacer>
          <v-card-actions>
            <v-btn :to="`/posts/${post.slug}`" variant="flat" class="w-full bg-[#26A69A] hover:!bg-[#00524B] hover:text-black text-white">
              <v-icon end>mdi-chevron-left</v-icon>
              مشاهده
            </v-btn>
          </v-card-actions>
        </v-card>
      </div>
    </div>
  </v-container>
</template>


<script setup>
import { useApi } from "../useApi";

const { request, fileUrl } = useApi();
const { data: posts, status, error } = await useApi().getPosts();
const router = useRouter();
const goToPost = (slug) => {
  router.push(`/posts/${slug}`);
};
</script>

<style scoped>
.blog-container {
  max-width: 1200px;
  margin: auto;
}

.blog-card {
  transition: transform 0.3s ease-in-out;
  border-radius: 12px;
  overflow: hidden;
}

.blog-card:hover {
  transform: scale(1.02);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.2);
}

.v-card-title {
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.v-btn {
  transition: background-color 0.3s ease-in-out;
}

.v-btn:hover {
  background-color: #0d47a1 !important;
  color: white !important;
}
</style>
