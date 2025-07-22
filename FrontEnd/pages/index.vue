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
    <div v-else class="!w-full !grid !grid-cols-3 gap-4 max-md:!grid-cols-2 max-sm:!grid-cols-1">
      <div v-for="(post,index) in posts" :key="post.id" class="w-full">
        <v-card class="elevation-3 blog-card group" v-if="index<6">
          <img :src="fileUrl + post.imageUrl" :alt="post.title" :title="post.title" class="!h-[200px] w-full object-cover rounded-t-lg group-hover:!scale-110 delay-3s duration-500  transition-all "></img>
          <v-card-title @click="goToPost(post.slug)" class="text-[#00524B] !font-bold">
            {{ post.title }}
          </v-card-title>
          <v-card-subtitle class="text-grey-darken-1 flex gap-2">
            {{ new moment(post.createdAt).format('jD jMMMM jYYYY')}}
            <div class="flex gap-1">
              <q-icon name="comment" class="text-[18px] text-primary"/>
              {{post.comments.length}}
            </div>
            <div class="flex gap-1">
            <q-icon name="visibility" class="text-[18px] text-[#219e00]"/>
            {{post.readCount}}
            </div>
          </v-card-subtitle>
          <v-card-text class="text-truncate">
            {{ post.description }}
          </v-card-text>
          <q-separator/>
          <v-card-actions class="!p-0">
            <div class="group cursor-pointer w-full flex items-center px-4 justify-between !h-[52px]" @click="router.push('/posts/' + post.slug)">
              <span class="group-hover:!text-[#0D9488]">ادامه مطلب</span>
              <q-icon name="arrow_back" class="group-hover:!text-[#0D9488] !text-[18px]"/>
            </div>
          </v-card-actions>
        </v-card>
      </div>
      <a :href="`/posts`" title="" class="transition-all hover:text-[#0D9488] duration-300 ease-in-out hover:-translate-y-0.5 cursor-pointer col-span-full flex items-center justify-center bg-white shadow-[0px_2px_13px_0px_rgba(0,0,0,0.02)] rounded-full p-4">نمایش تمامی مطالب</a>
    </div>
  </div>
</template>

<script setup>
import { useApi } from "../useApi";
import moment from 'moment-jalaali'

const { request, fileUrl } = useApi();
const { data: posts, status, error } = await useApi().getPosts();
const showAll = ref(false);
const showMoreFn = () =>{
  showAll.value = true;
}
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
  @apply shadow-[0px_2px_13px_0px_rgba(0,0,0,0.02)]

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
