
<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router'
import moment from 'moment-jalaali'
import { computed, ref, onMounted, onUnmounted } from 'vue'
import { useQuasar } from 'quasar'
import Footer from '../layouts/footer.vue'

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
</script>
<template>
  <v-app>
    <v-app-bar class="!shadow-[0_2px_8px_0px_rgba(99,99,99,0.1)] !bg-[#00524B] !p-3 !w-full !h-[78px] !flex !justify-between !items-center !shrink-0">
      <span class="text-white text-justify">وبلاگ یک حسابدار</span>
      <v-spacer></v-spacer>
      <img
          alt="yek hesabdar"
          class="w-[90px] invert brightness-0 cursor-pointer me-3"
          src="/images/yekhesabdar.webp"
          @click="router.push('/')"
      />
    </v-app-bar>
    <v-main>
      <v-container class="!pt-10">
        <NuxtPage />
      </v-container>
    </v-main>
    <div>
      <component :is="Footer" />
    </div>
  </v-app>
</template>

<style>
</style>
