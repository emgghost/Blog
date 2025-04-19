<template>
  <v-navigation-drawer v-model="drawer" app>
    <v-list>
      <v-list-item
        prepend-icon="mdi-view-dashboard"
        title="داشبورد"
        color="primary"
        to="/admin"
        :active="currentPath === '/admin'"
      />
      <v-list-item
        prepend-icon="mdi-post"
        title="مدیریت پست‌ها"
        to="/admin/posts"
        color="primary"
        :active="currentPath.startsWith('/admin/posts')"
      />
      <v-list-item
        prepend-icon="mdi-shape"
        title="دسته‌بندی‌ها"
        to="/admin/categories"
        color="primary"
        :active="currentPath.startsWith('/admin/categories')"
      />
      <v-list-item
        prepend-icon="mdi-tag"
        color="primary"
        title="برچسب‌ها"
        to="/admin/tags"
        :active="currentPath.startsWith('/admin/tags')"
      />
      <v-list-item
        prepend-icon="mdi-logout"
        title="خروج"
        @click="handleLogout"
        color="error"
      />
    </v-list>
  </v-navigation-drawer>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'

const props = defineProps({
  modelValue: {
    type: Boolean,
    required: true
  }
})

const emit = defineEmits(['update:modelValue'])

const router = useRouter()
const route = useRoute()

const currentPath = computed(() => route.path)

const drawer = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', value)
})

const handleLogout = () => {
  localStorage.removeItem('token')
  router.push('/login')
}
</script>
<style scoped>
</style>
