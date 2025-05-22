<!-- pages/categories/index.vue -->
<template>
  <v-container>
    <v-btn to="/categories/create" color="primary">ایجاد دسته‌بندی جدید</v-btn>
    <v-list>
      <v-list-item v-for="category in categories" :key="category.id">
        <v-list-item-title>{{ category.name }}</v-list-item-title>
        <v-btn :to="`/categories/edit?id=${category.id}`" icon>
          <v-icon>mdi-pencil-outline</v-icon>
        </v-btn>
      </v-list-item>
    </v-list>
  </v-container>
</template>

<script>
import { useApi } from '../../useApi';
const api = useApi();

export default {

  data() {
    return {
      categories: []
    }
  },
  async fetch() {

    this.categories = (await api.getCategories()).data
  }
}
</script>
