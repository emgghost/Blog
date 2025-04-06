<script lang="ts" setup>
import LazySvgAparat from '../components/svg/aparat.vue'
import LazySvgInstagram from '../components/svg/instagram.vue'
import LazySvgTelegram from '../components/svg/telegram.vue'
import LazySvgVirgol from '../components/svg/virgol.vue'
const socials = [
  { title: 'کانال تلگرامی یک حسابدار', url: 'https://t.me/yekhesabdar_academy', icon: LazySvgTelegram },
  { title: 'ویرگول یک حسابدار', url: 'https://virgool.io/@yekhesabdar', icon: LazySvgVirgol },
  {
    title: 'صفحه اینستاگرامی یک حسابدار',
    url: 'https://www.instagram.com/yekhesabdar?igsh=MWg4YmN6djBlNGdvMA==',
    icon: LazySvgInstagram
  },
  { title: 'صفحه آپارات یک حسابدار', url: 'https://www.aparat.com/yekhesabdar.com', icon: LazySvgAparat }
]
</script>

<template>
  <footer class="w-full bg-gradient-to-b to-[#042e2a] from-[#0d9488]">
    <div class="w-full bg-[url('/images/wallpaper/Vector.svg')]">
      <div
          class="container mx-auto w-full max-md:flex-col md:max-w-[70vw] py-6 flex max-md:items-center md:items-start justify-between gap-6"
      >
        <div class="flex flex-col gap-4 w-full max-md:px-2 md:w-1/2">
          <div class="flex justify-between">
            <img alt="yek hesabdar" class="w-[150px] invert brightness-0" src="/images/yekhesabdar.webp" />
            <img alt="nevis taraz" class="w-[150px] invert brightness-0" src="/images/novin.webp" />
          </div>
          <span class="text-2xl text-white max-md:text-xl">آکادمی آموزش حسابداری و مالیات یک حسابدار</span>
          <span class="text-sm text-white flex-wrap text-justify text-weight-thin"
          >شرکت نویس تراز پارسه ایرانیان با مدیریت امیرحسین کوچک زاده از سال 1400 تحت عنوان برند آکادمی یک حسابدار فعالیت خویش را در جهت
            ارائه خدمات تخصصی در زمینه مالی و مالیاتی آغاز نمود.</span
          >
          <span class="text-sm text-white flex-wrap text-justify text-weight-thin">
            این آکادمی در ادامه فعالیت خود موفق به طراحی و ایجاد نرم افزار واسط صدور صورت حساب الکترونیک در جهت بهبود فرایند پیاده سازی
            قانون پایانه های فروشگاهی و سامانه مودیان شده است.</span
          >
          <span class="text-sm text-white text-weight-thin">
            <q-icon name="location_on" />
            شعبه مرکزی: قم، خیابان سمیه بین کوچه ۴ و ۶ ساختمان رفعت، طبقه اول، واحد دوم
          </span>
          <span class="text-sm text-white text-weight-thin">
            <q-icon name="mail" />
            info@yekhesabdar.com
          </span>
          <span class="text-white text-sm text-weight-thin">
            <q-icon name="phone" class="rotate-270" />
            025-37738412
          </span>
          <span class="text-white text-sm text-weight-thin">
            <q-icon name="phone" class="rotate-270" />
            025-37737912
          </span>
          <div class="flex gap-2 text-white text-sm">
            <a v-for="item in socials" :href="item.url" class="leading-[2] text-white" target="_blank">
              <component :is="item.icon" class="fill-white w-6" />
              <q-tooltip>
                {{ item.title }}
              </q-tooltip>
            </a>
          </div>
        </div>
        <!--        <img class="w-32" src="/enemad.png" alt="electronic trust sign" />-->
      </div>
      <div class="w-full">
        <div class="container mx-auto max-w-[65vw] flex justify-center">
          <p class="text-white py-2 text-weight-thin">تمام حقوق این سایت برای یک حسابدار محفوظ است.</p>
        </div>
      </div>
    </div>
  </footer>
</template>
<style scoped >
</style>
