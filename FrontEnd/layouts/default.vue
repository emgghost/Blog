
<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router'
import moment from 'moment-jalaali'
import { computed, ref, onMounted, onUnmounted } from 'vue'
import { useQuasar } from 'quasar'
import Footer from '../layouts/footer.vue'
import { Swiper, SwiperSlide } from 'swiper/vue';
import { NuxtLink } from '#components'
// Import Swiper styles
import 'swiper/css';
import { useApi } from "../useApi";

import 'swiper/css/navigation';


// import required modules
import { Navigation } from 'swiper/modules';
const modules = [Navigation];
const router = useRouter()

// Define Persian date format
moment.loadPersian({ usePersianDigits: true, dialect: 'persian-modern' })

// Format date in Persian (Jalaali) calendar: "dddd jD jMMMM jYYYY" for full date
const date = ref(moment().format('dddd jD jMMMM jYYYY'))

// Ref for time
const time = ref('')

// Function to update time
const updateTime = () => {
  const now = moment()
  time.value = now.format('h:mm A').replace('AM', 'ق.ظ').replace('PM', 'ب.ظ')
}

// Initial time setup
updateTime()

// Update time every minute
let timer: NodeJS.Timeout
onMounted(() => {
  timer = setInterval(updateTime, 60000)
})

onUnmounted(() => {
  clearInterval(timer)
})

const $q = useQuasar()
const route = useRoute()
const isMenuOpen = ref(false)
const isSubMenuOpen = ref(false)
const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value
}
const toggleSubMenu = () => {
  isSubMenuOpen.value = !isSubMenuOpen.value
}
const menuList = [
  { label: 'خانه', url: '/', show: true },
  { label: 'محصولات', url: null, show: true,subList: [{
      label:'نرم افزار واسط یک حسابدار',url:'https://yekhesabdar.com/kit/products'
    }
    ] },
  {
    label: 'سامانه مودیان',
    url: null,
    show: true,
    subList: [
      { label: 'معرفی نرم افزار واسط یک حسابدار', url: 'https://yekhesabdar.com/kit/about-app' },
      { label: 'آموزش های نرم افزار واسط یک حسابدار', url: 'https://yekhesabdar.com/kit/tutorials' },
      { label: 'صدور csr و کلید عمومی و خصوصی', url: 'https://yekhesabdar.com/kit/key-generate' },
      { label: 'صدور کلید عمومی و خصوصی بر اساس csr', url: 'https://yekhesabdar.com/kit/key-with-csr' },
      { label: 'استعلام کد اقتصادی', url: 'https://yekhesabdar.com/kit/economic' },
      { label: 'سامانه هوشمند شناسه کالا / خدمات', url: 'https://yekhesabdar.com/kit/stuff' },
      { label: 'سوالات متداول سامانه مودیان', url: 'https://yekhesabdar.com/kit/faq-moadian' },
      { label: 'اطلاعیه های سامانه مودیان', url: 'https://yekhesabdar.com/kit/news-moadian' },
      { label: 'بروزرسانی های نرم افزار واسط یک حسابدار', url: 'https://yekhesabdar.com/kit/journey' },
    ]
  },
  // { label: 'سوالات متداول', url: '/#faq', show: true },
  // { label: 'بلاگ', url: '/kit/blog', show: true },
  { label: 'خدمات ما', url: 'https://yekhesabdar.com/kit/services', show: true },
  { label: 'درباره ما', url: 'https://yekhesabdar.com/kit/about-us', show: true }
]
watch(()=>route.name,
    ()=>{
      isMenuOpen.value = false
    }
)

const { data: posts, status, error } = await useApi().getPosts();
const { request, fileUrl } = useApi();
</script>
<template>
  <v-app>
    <v-app-bar class="!shadow-[0_2px_8px_0px_rgba(99,99,99,0.1)] !bg-[#00524B] !p-3 !w-full !h-[78px] !flex !justify-between !items-center !shrink-0">
        <!-- Left side with Hamburger Menu and Logo -->
        <div class="flex items-center justify-center">
          <!-- Hamburger Icon for small screens -->
          <div class="flex w-fit h-full justify-center items-center lg:hidden relative">
            <lazy-q-icon
                class="icon text-white text-2xl p-2 transition-transform duration-300 hidden max-lg:!block"
                :class="isMenuOpen ? 'translate-x-[250px]' : 'translate-x-0'"
                name="menu"
                @click="toggleMenu()"
            />
          </div>
          <!-- Logo -->
          <router-link to="/">
            <img alt="yek hesabdar" class="w-20 invert brightness-0" src="/images/yekhesabdar.webp" />
          </router-link>
        </div>

        <!-- Menu List (Visible on large screens) -->
        <div class="flex justify-between items-center mx-auto menu max-lg:!hidden">
          <ul class="container max-w-[90vw] text-white 2xl:max-w-[65vw] pe-20 mx-auto">
            <template v-for="item in menuList">
              <li v-if="item.show">
                <nuxt-link v-if="!!item.url && item.url === '/'" @click="navigateTo(item.url,{external:true})" class="item cursor-pointer">
                  {{ item.label }}
                  <q-icon v-if="item.subList" name="expand_more" />
                </nuxt-link>
                <a v-if="!!item.url && item.url !== '/'" target="_blank" :href="item.url"  class="item">
                  {{ item.label }}
                  <q-icon v-if="item.subList" name="expand_more" />
                </a>
                <span v-else-if="item.url !== '/'" class="item cursor-pointer">
            {{ item.label }}
            <q-icon v-if="item.subList" name="expand_more" />
          </span>
                <div v-if="!!item.subList" class="sub-menu !top-0">
                  <ul>
                    <li v-for="sub in item.subList">
                      <a v-if="!!sub.url" :href="sub.url" target="_blank" class="item">
                        {{ sub.label }}
                      </a>
                      <span v-else class="item" v-text="sub.label"></span>
                    </li>
                  </ul>
                </div>
              </li>
            </template>
          </ul>
        </div>
        <!-- Sidebar Menu (Visible on small screens when toggled) -->
        <div
            class="fixed menu top-0 right-0 h-fit w-[300px] bg-white shadow-lg z-20 transform transition-transform duration-300 border border-transparent rounded-xl rounded-tr-none"
            :class="isMenuOpen ? 'translate-x-0' : 'translate-x-full'"
        >
          <ul class="container flex flex-col items-start px-6 py-6 text-black gap-2">
            <template v-for="item in menuList">
              <li v-if="item.show" class="w-full">
                <nuxt-link v-if="!!item.url" :to="item.url" class="!h-[48px] item w-full" :class="route.path===item.url ? 'bg-teal-800 rounded-lg !text-white':'border'">
                  {{ item.label }}
                  <q-icon v-if="item.subList" name="expand_more" />
                </nuxt-link>
                <span v-else class="item w-full !h-[48px]" @click="toggleSubMenu" :class="route.path===item.url ? 'bg-primary rounded-lg border-primary ':'border'">
              {{ item.label }}
              <q-icon v-if="item.subList" name="expand_more" />
            </span>
                <ul v-if="isSubMenuOpen && !!item.subList" class="flex flex-col items-start pe-6 pt-2 ms-2 text-[#6F6F6F] gap-2">
                  <li v-for="sub in item.subList" class="w-full ps-2 !h-[48px] flex" :class="route.path===sub.url ? 'bg-primary rounded-lg border-primary ':'border rounded-lg'">
                    <lazy-nuxt-link v-if="!!sub.url" :to="sub.url" class="!h-[48px] flex items-center !w-full">
                      {{ sub.label }}
                    </lazy-nuxt-link>
                    <span v-else class="item w-full !h-[48px]" v-text="sub.label"></span>
                  </li>
                </ul>
              </li>
            </template>
          </ul>
        </div>
        <!-- Overlay when menu is open -->
        <div
            v-if="isMenuOpen"
            class="fixed inset-0 bg-transparent z-10"
            @click="toggleMenu"></div>
    </v-app-bar>
    <v-main class="!bg-[#edf3f5]">
      <swiper :navigation="true" :modules="modules" class="mySwiper !h-[500px] mb-14" v-if="route.path === '/'">
        <swiper-slide v-for="post in posts.filter((item)=>item.addToSlider===true)">
          <img :alt="post.title" :title="post.title" :src="!!post.sliderImageUrl? fileUrl + post.sliderImageUrl : fileUrl + post.imageUrl" class="h-[500px] w-full rounded-t-lg group-hover:!scale-110 delay-3s duration-500  transition-all"/>
          <div class="flex-col gap-2 w-[500px] h-[150px] bg-white absolute top-full left-1/2 -translate-x-1/2 -translate-y-1/3 rounded-xl flex items-center justify-center">
            <div v-if="!!post.categories.length" class="w-full justify-center gap-1 flex">
              <a v-for="item in post.categories" :href="`/posts?category=${item.slug}`" class="w-fit font-thin flex h-full items-center justify-center cursor-pointer p-1 border border-gray-400 bg-white hover:bg-[#0D9488]/80 transition-all duration-500 text-black hover:text-white rounded-full">
              {{item.name}}
              </a>
            </div>
            <a :href="`/posts/${post.slug}`" :title="post.title" class="hover:text-[#0D9488] transition-all ease-in-out duration-500 cursor-pointer">
              {{ post.title }}
            </a>
            <div class="w-full flex !text-gray-400 font-thin justify-center gap-3">
              {{ new moment(post.createdAt).format('jD jMMMM jYYYY')}}
              <div class="flex gap-1 items-center">
                <q-icon name="comment" class="text-[18px] text-primary"/>
                {{post.comments.length}}
              </div>
              <div class="flex gap-1 items-center">
                <q-icon name="visibility" class="text-[18px] text-[#219e00]"/>
                {{post.readCount}}
              </div>
            </div>
          </div>
        </swiper-slide>
      </swiper>
      <v-container class="!pt-10">
        <NuxtPage />
      </v-container>
    </v-main>
    <div>
      <component :is="Footer" />
    </div>
  </v-app>
</template>

<style scoped>

.swiper {
  width: 100%;
  height: 100%;
}

.swiper-slide {
  text-align: center;
  font-size: 18px;
  background: #fff;

  /* Center slide text vertically */
  display: flex;
  justify-content: center;
  align-items: center;
}

.swiper-slide img {
  display: block;
  width: 100%;
  height: 100%;
  object-fit: cover;
}
:deep(.mySwiper) {
  overflow-y: visible !important;
  padding-bottom: 100px !important;
}

:deep(.v-toolbar__content) {
  overflow: visible !important;
}

:deep(.swiper-slide) {
  overflow-y: visible !important; /* let children like title boxes overflow within each slide */
  position: relative;
}
</style>
