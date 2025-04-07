<template>
  <div class="w-full h-full flex justify-center">
    <!-- وضعیت بارگذاری -->
    <v-row v-if="!status">
      <v-col v-for="n in 4" :key="n" cols="12" md="6">
        <v-skeleton-loader type="image, article, button"></v-skeleton-loader>
      </v-col>
    </v-row>

    <!-- نمایش خطا -->
    <v-alert v-else-if="error" type="error" variant="tonal" class="text-center">
      ❌ خطا در دریافت داده‌ها: {{ error.message }}
    </v-alert>

    <!-- نمایش لیست پست‌ها -->
    <div v-else class="!w-full !grid !grid-cols-2 gap-4">
      <div v-for="post in posts" :key="post.id" class="w-full">
        <v-card class="elevation-3 blog-card">
          <v-img :src="fileUrl + post.imageUrl" height="200px" cover class="rounded-t-lg"></v-img>
          <v-card-title @click="goToPost(post.slug)" class="text-[#00524B] !font-bold">
            {{ post.title }}
          </v-card-title>
          <v-card-subtitle class="text-grey-darken-1">
            {{ new moment(post.createdAt).format('jD jMMMM jYYYY')}}
          </v-card-subtitle>
          <v-card-text class="text-truncate">
            {{ post.description }}
          </v-card-text>
          <q-separator/>
          <v-card-actions class="!p-0">
<!--            <q-btn-->
<!--                class="w-full bg-[#00524B] !text-white my-auto h-[40px] shrink-0 rounded-lg flex"-->
<!--                flat-->
<!--                label="ادامه مطلب"-->
<!--                icon-right="arrow_back"-->
<!--                push-->
<!--                @click="router.push('/posts/' + post.slug)"-->
<!--            />-->
            <div class="group cursor-pointer w-full flex items-center px-4 justify-between !h-[52px]" @click="router.push('/posts/' + post.slug)">
              <span class="group-hover:!text-[#0D9488]">ادامه مطلب</span>
              <q-icon name="arrow_back" class="group-hover:!text-[#0D9488]"/>
            </div>
          </v-card-actions>
        </v-card>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useApi } from "../useApi";
import moment from 'moment-jalaali'

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
