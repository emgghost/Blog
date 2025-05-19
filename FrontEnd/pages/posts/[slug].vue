<template>
  <div class="w-full h-full grid grid-cols-4 gap-4">
    <div class="col-span-3 max-md:col-span-full">
      <q-skeleton v-if="!status" height="400px" square class="full-width"/>
      <div v-else class="w-full p-1">
        <!-- Blog Title -->
        <a class="text-h4 text-primary text-center q-my-md" :href="`/posts/${post.slug}`" :title="post.title">
          {{ post.title }}
        </a>
        <!-- Blog Author -->
        <div class="flex gap-2 row mb-2">
          <span v-if="!post.author" class="text-[#888f96]">
             نویسنده :
          </span>
          <span v-if="!post.author">
             {{ post.author}}
          </span>
          <span v-if="!post.author" class="text-[#888f96]">
             ــــ
          </span>
          <span v-if="!!post.createdAt" class="text-[#888f96]">
            {{ new moment(post.createdAt).format('jD jMMMM jYYYY')}}
          </span>
        </div>
        <!-- Blog Image -->
        <div class="row w-full">
          <img
              :alt="post.title"
              :title="post.title"
              :src="fileUrl + post.imageUrl"
              class="blog-image w-full"
          />
        </div>
        <!-- Blog Stats -->
        <div class="q-my-md w-fit px-4 flex border bg-white rounded-full justify-center items-center gap-2">
          <div class="flex gap-1 !border-l px-3 py-2 border-gray-200 items-center">
            <q-icon name="comment" class="text-[18px] text-primary"/>
            <span class="text-[18px] text-[#888f96]">{{post.comments.length}}</span>
          </div>
          <div class="flex gap-1 px-3 py-2 items-center">
            <q-icon name="visibility" class="text-[18px] text-[#219e00]"/>
            <span class="text-[18px] text-[#888f96]">{{post.readCount}}</span>
          </div>
        </div>
        <!-- Blog Content -->
        <div class="q-my-md text-body1 blog-content" v-html="post.content"></div>

        <!-- Categories -->
        <div class="row items-center q-mt-md" v-if="!!post.categories.length">
          دسته بندی ها :
          <q-chip
              v-for="category in post.categories"
              :key="category.id"
              class="q-ma-sm blog-chip bg-white rounded-full hover:!bg-orange-600 transition-all duration-200 cursor-pointer hover:text-white"
          >
            {{ category.name }}
          </q-chip>
        </div>
        <!-- Tags -->
        <div class="row items-center q-mt-md border p-2 rounded-lg" v-if="!!post.tags.length">
          برچسب ها :
          <q-chip
              v-for="tag in post.tags"
              :key="tag.id"
              class="q-ma-sm blog-chip bg-blue-100 rounded-full hover:!bg-blue-500 transition-all duration-200 cursor-pointer hover:text-white"
          >
            {{ tag.name }}
          </q-chip>
        </div>
      </div>
      <div class="w-full min-h-[500px] grid grid-cols-3 gap-2 max-md:grid-cols-2 max-sm:grid-cols-1">
        <div class="flex w-full items-center col-span-full ">
          <h1 class="text-bold text-[24px] w-fit">ممکن است علاقه داشته باشید</h1>
          <q-icon name="arrow_back" class="text-[24px]"/>
        </div>
        <div v-for="(item,index) in posts.filter((blog)=>blog.id !== post.id)" :key="item.id" class="col-span-1 cursor-pointer" @click="()=>router.push(`${item.slug}`)">
          <v-card class="elevation-3 blog-card group" v-if="index < 3 && item.id!==post.id">
            <v-img :src="fileUrl + item.imageUrl" height="200px" cover
                   class="rounded-t-lg group-hover:!scale-110 delay-3s duration-500  transition-all "></v-img>
            <v-card-title class="text-[#00524B] !font-bold">
              {{ item.title }}
            </v-card-title>
            <v-card-subtitle class="flex gap-2">
              {{ new moment(item.createdAt).format('jD jMMMM jYYYY') }}
              <div class="flex gap-1">
                <q-icon name="comment" class="text-[18px] text-primary"/>
                {{item.comments.length}}
              </div>
              <div class="flex gap-1">
                <q-icon name="visibility" class="text-[18px] text-[#219e00]"/>
                {{item.readCount}}
              </div>
            </v-card-subtitle>
            <v-card-text class="text-truncate">
              {{ item.description }}
            </v-card-text>
            <q-separator/>
            <v-card-actions class="!p-0">
              <div class="group cursor-pointer w-full flex items-center px-4 justify-between !h-[52px]"
                   @click="goToPost(item.slug)">
                <span class="group-hover:!text-[#0D9488]">ادامه مطلب</span>
                <q-icon name="arrow_back" class="group-hover:!text-[#0D9488] !text-[18px]"/>
              </div>
            </v-card-actions>
          </v-card>
        </div>
      </div>
    </div>
    <div class="post-sidebar max-md:col-span-full col-span-1 w-full h-full">
      <q-card>
        <q-tabs
            v-model="tab"
            dense
            no-caps
            class="text-grey"
            active-color="negative"
            indicator-color="negative"
            align="justify"
        >
          <q-tab name="favorites" label="محبوب ها"/>
          <q-tab name="new" label="ترند ها"/>
        </q-tabs>

        <q-separator/>

        <q-tab-panels v-model="tab" animated>
          <q-tab-panel name="favorites">
            <div class="text-h6">پر بازدید ترین مطالب</div>
            <div v-for="(item,index) in posts.filter((blog)=>blog.id !== post.id).sort((a,b)=>b.readCount - a.readCount).slice(0,3)" :key="item.id" class="w-full my-1">
              <a class="h-[100px] flex group cursor-pointer" :href="`/posts/${item.slug}`">
                <div class="w-full flex items-center" :class="index !== 2 ? 'border-b':''">
                  <v-card-title class="text-[#00524B] w-3/4 !font-bold text-wrap text-[16px]">
                    {{ item.title }}
                  </v-card-title>
                  <v-img :src="fileUrl + item.imageUrl"
                         class="!max-w-[96px] !max-h-[72px] rounded-t-lg group-hover:!scale-105 delay-3s duration-500 overflow-hidden  transition-all "/>
                </div>
              </a>
            </div>
          </q-tab-panel>

          <q-tab-panel name="new">
            <div class="text-h6">تازه ترین ها</div>
            <div v-for="(item,index) in posts.filter((blog)=>blog.id !== post.id).sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt)).slice(0,3)" :key="item.id" class="w-full my-1">
              <div class="h-[100px] flex group cursor-pointer" @click="()=>router.push(`${item.slug}`)">
                <div class="w-full flex items-center" :class="index !== 2 ? 'border-b':''">
                  <v-card-title @click="goToPost(item.slug)" class="text-[#00524B] w-3/4 !font-bold text-wrap text-[16px]">
                    {{ item.title }}
                  </v-card-title>
                  <v-img :src="fileUrl + item.imageUrl"
                         class="!max-w-[96px] !max-h-[72px] rounded-t-lg group-hover:!scale-105 delay-3s duration-500 overflow-hidden  transition-all "/>
                </div>
              </div>
            </div>
          </q-tab-panel>
        </q-tab-panels>
      </q-card>
    </div>
  </div>
</template>

<script setup>
import {useApi} from '../../useApi';
import {useRoute} from 'vue-router';
import moment from "moment-jalaali";
const api = useApi();
const fileUrl = api.fileUrl;
const tab = ref('favorites')
const {data: posts, status: postStatuses, error} = await useApi().getPosts();
const router = useRouter();
const route = useRoute();
// گرفتن اطلاعات پست با استفاده از API
const {data: post, status} = await api.getPostBySlug(route.params.slug);
const seoKeyWords = ref()
const generateSeoKeywords = ()=>{
  if (post.value.tags.length) {
    seoKeyWords.value = post.value.tags.map((item)=> item.name.trim()).filter(Boolean).join(', ')
  }
}
watch(() => post.value, () => {
  generateSeoKeywords();
}, { immediate: true });
useHead({
  title:`${route.params.slug.replace('-',' ')} - وبلاگ یک حسابدار`,
  meta: [
    { name:'description',content:'وبلاگ آموزشی یک حسابدار با هدف آموزش مبانی مالیاتی و سامانه مودیان مالیاتی در خدمت شماست.' },
    {
      name: 'keywords',
      content: computed(() => seoKeyWords.value)
    },
  ]
})
const goToPost = (slug) => {
  router.push(`/posts/${slug}`);
};
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
  object-fit: contain;
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
