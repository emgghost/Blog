<script setup>

import moment from "moment-jalaali";
import {useApi} from "../../useApi";
import {useRoute} from "vue-router";
const { request, fileUrl,getPostByCategory,getPostByTag } = useApi();
const posts = ref(null)
const data = ref(null)
onBeforeMount(async ()=>{
  if (route.query.category) {
    data.value = await getPostByCategory(route.query.category);
    posts.value = data.value.data.blogPosts
  }
  else if (route.query.tag) {
    data.value = await getPostByTag(route.query.tag);
    posts.value = data.value.data.blogPosts
  }else {
    data.value = await useApi().getPosts();
    posts.value = data.value.data
  }
})
const router = useRouter();
const route = useRoute();
const goToPost = (slug) => {
  router.push(`/posts/${slug}`);
};
</script>

<template>
  <div class="flex w-full">
    <h1 v-if="route.query.category" class="font-bold text-[26px] border rounded-xl w-full px-2 mb-2">مطالب منتشر شده در وبلاگ یک حسابدار با موضوع {{route.query.category}}</h1>
    <h1 v-else-if="route.query.tag" class="font-bold text-[26px] border rounded-xl w-full px-2 mb-2">مطالب منتشر شده در وبلاگ یک حسابدار با برچسب {{route.query.tag}}</h1>
    <h1 v-else class="font-bold text-[26px] border rounded-xl w-full px-2 mb-2">تمامی مطالب منتشر شده در وبلاگ یک حسابدار</h1>
  </div>
  <div class="!w-full !grid !grid-cols-3 gap-4 max-md:!grid-cols-2 max-sm:!grid-cols-1">
    <div  v-for="(post,index) in posts" :key="post.id" class="w-full">
      <v-card class="elevation-3 blog-card group">
        <img :src="fileUrl + post.imageUrl" :alt="post.title" :title="post.title" class="!h-[200px] w-full object-cover rounded-t-lg group-hover:!scale-110 delay-3s duration-500  transition-all "/>
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
  </div>
</template>

<style scoped>

</style>