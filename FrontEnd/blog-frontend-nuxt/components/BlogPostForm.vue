<!-- components/BlogPostForm.vue -->
<template>
  <v-select
    v-model="selectedCategories"
    :items="categories"
    item-text="name"
    item-value="id"
    label="دسته‌بندی‌ها"
    multiple
  ></v-select>
</template>

<script>
export default {
  data() {
    return {
      categories: [],
      selectedCategories: []
    }
  },
  async fetch() {
    this.categories = await this.$axios.$get('/api/categories')
  }
}
</script>
