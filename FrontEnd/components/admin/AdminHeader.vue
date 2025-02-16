<template>
  <v-app-bar app>
    <v-app-bar-nav-icon @click="$emit('toggle-drawer')"></v-app-bar-nav-icon>
    <v-toolbar-title>{{ title }}</v-toolbar-title>
    <v-spacer></v-spacer>
    <v-btn icon @click="toggleTheme">
      <v-icon>{{ isDark ? 'mdi-weather-sunny' : 'mdi-weather-night' }}</v-icon>
    </v-btn>
  </v-app-bar>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const isDark = ref(false)

const title = computed(() => {
  const path = route.path
  if (path === '/admin') return 'داشبورد'
  if (path.startsWith('/admin/posts')) return 'مدیریت پست‌ها'
  if (path.startsWith('/admin/categories')) return 'مدیریت دسته‌بندی‌ها'
  if (path.startsWith('/admin/tags')) return 'مدیریت برچسب‌ها'
  return 'پنل مدیریت'
})

const toggleTheme = () => {
  isDark.value = !isDark.value
  // You can implement theme switching logic here
}

defineEmits(['toggle-drawer'])
</script>
