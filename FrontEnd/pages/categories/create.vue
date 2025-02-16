<!-- pages/categories/create.vue -->
<template>
  <v-container>
    <v-form @submit.prevent="createCategory">
      <v-text-field v-model="category.name" label="نام دسته‌بندی"></v-text-field>
      <v-btn type="submit" color="primary">ذخیره</v-btn>
    </v-form>
  </v-container>
</template>

<script>
import api from 'useApi';

export default {
  data() {
    return {
      category: {
        name: ''
      }
    }
  },
  methods: {
    async createCategory() {
      await api.createCategory(this.category)
      this.$router.push('/categories')
    }
  }
}
</script>
