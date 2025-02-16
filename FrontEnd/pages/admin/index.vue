<!-- pages/admin/index.vue -->
<template>
  <v-container>
    <h1>پنل مدیریت</h1>
    <v-tabs>
      <v-tab to="/admin/posts">پست‌ها</v-tab>
      <v-tab to="/admin/categories">دسته‌بندی‌ها</v-tab>
      <v-tab to="/admin/tags">برچسب‌ها</v-tab>
    </v-tabs>
    <nuxt-child />
  </v-container>
</template>

<script>
definePageMeta({
  layout: 'admin',
  middleware: ['auth']
})
export default {
  middleware: 'auth'
};
</script>
