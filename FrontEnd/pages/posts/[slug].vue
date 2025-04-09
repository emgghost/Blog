<template>
  <div class="w-full h-full grid grid-cols-4 gap-4">
    <div class="col-span-3">
      <q-skeleton v-if="!status" height="400px" square class="full-width"/>
      <div v-else class="w-full p-1">
        <!-- Blog Image -->
        <q-img
            :src="fileUrl + post.imageUrl"
            class="blog-image"
            style="height: 400px; object-fit: cover"
            :ratio="16/9"
        />

        <!-- Blog Title -->
        <div class="text-h4 text-primary text-center q-my-md">
          {{ post.title }}
        </div>

        <!-- Blog Content -->
        <div class="q-my-md text-body1 blog-content" v-html="post.content"></div>

        <!-- Categories -->
        <div class="row justify-center q-mt-md">
          <q-chip
              v-for="category in post.categories"
              :key="category.id"
              color="primary"
              class="q-ma-sm blog-chip"
              outline
          >
            {{ category.name }}
          </q-chip>
        </div>
      </div>
      <div class="w-full h-[500px] grid grid-cols-3 gap-2">
        <div class="flex w-full items-center col-span-full ">
          <h1 class="text-bold text-[24px] w-fit">ممکن است علاقه داشته باشید</h1>
          <q-icon name="arrow_back" class="text-[24px]"/>
        </div>
        <div v-for="(post,index) in posts" :key="post.id" class="col-span-1">
          <v-card class="elevation-3 blog-card group" v-if="index < 3">
            <v-img :src="fileUrl + post.imageUrl" height="200px" cover
                   class="rounded-t-lg group-hover:!scale-110 delay-3s duration-500  transition-all "></v-img>
            <v-card-title @click="goToPost(post.slug)" class="text-[#00524B] !font-bold">
              {{ post.title }}
            </v-card-title>
            <v-card-subtitle class="text-grey-darken-1">
              {{ new moment(post.createdAt).format('jD jMMMM jYYYY') }}
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
              <div class="group cursor-pointer w-full flex items-center px-4 justify-between !h-[52px]"
                   @click="routered.push('/posts/' + post.slug)">
                <span class="group-hover:!text-[#0D9488]">ادامه مطلب</span>
                <q-icon name="arrow_back" class="group-hover:!text-[#0D9488] !text-[18px]"/>
              </div>
            </v-card-actions>
          </v-card>
        </div>
      </div>
    </div>
    <div class="post-sidebar col-span-1 w-full h-full">
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
          <q-tab name="mails" label="محبوب ها"/>
          <q-tab name="alarms" label="ترند ها"/>
        </q-tabs>

        <q-separator/>

        <q-tab-panels v-model="tab" animated>
          <q-tab-panel name="mails">
            <div class="text-h6">Mails</div>
            Lorem ipsum dolor sit amet consectetur adipisicing elit.
          </q-tab-panel>

          <q-tab-panel name="alarms">
            <div class="text-h6">Alarms</div>
            Lorem ipsum dolor sit amet consectetur adipisicing elit.
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

const router = useRoute();
const api = useApi();
const fileUrl = api.fileUrl;
const tab = ref('mails')
const {data: posts, status: postStatuses, error} = await useApi().getPosts();
// گرفتن اطلاعات پست با استفاده از API
const {data: post, status} = await api.getPostBySlug(router.params.slug);

const routered = useRouter();
const goToPost = (slug) => {
  routered.push(`/posts/${slug}`);
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
