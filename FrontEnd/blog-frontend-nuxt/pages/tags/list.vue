<!-- pages/tags/index.vue -->
<template>
  <v-container>
    <v-btn to="/tags/create" color="primary">ایجاد دسته‌بندی جدید</v-btn>
    <v-list>
      <v-list-item v-for="tag in tags" :key="tag.id">
        <v-list-item-title>{{ tag.name }}</v-list-item-title>
        <v-btn :to="`/tags/edit?id=${tag.id}`" icon>
          <v-icon>mdi-pencil-outline</v-icon>
        </v-btn>
      </v-list-item>
    </v-list>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      tags: []
    }
  },
  async fetch() {
    this.tags = await this.$axios.$get('/tags')
  }
}
</script>
