<template>
  <v-navigation-drawer v-model="drawer" app>
    <v-list>
      <v-list-item
        prepend-icon="mdi-view-dashboard"
        title="داشبورد"
        to="/admin"
        :active="currentPath === '/admin'"
        :active-color="'white'"
      />
      <v-list-item
        prepend-icon="mdi-post"
        title="مدیریت پست‌ها"
        to="/admin/posts"
        :active="currentPath.startsWith('/admin/posts')"
        :active-color=re
      />
      <v-list-item
        prepend-icon="mdi-shape"
        title="دسته‌بندی‌ها"
        to="/admin/categories"
        :active="currentPath.startsWith('/admin/categories')"
        :active-color="'white'"
      />
      <v-list-item
        prepend-icon="mdi-tag"
        title="برچسب‌ها"
        to="/admin/tags"
        :active="currentPath.startsWith('/admin/tags')"
        :active-color="'white'"
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
