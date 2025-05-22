<!-- pages/categories/create.vue -->
<template>
  <v-container>
    <v-form @submit.prevent="createtag">
      <v-text-field v-model="tag.name" label="نام تگ"></v-text-field>
      <v-btn type="submit" color="primary">ذخیره</v-btn>
    </v-form>
  </v-container>
</template>

<script>
import { useApi } from '../../useApi';
const api = useApi();

export default {
  data() {
    return {
      tag: {
        name: ''
      }
    }
  },
  methods: {
    async createTag() {
      await api.createTag(this.tag)
      this.$router.push('/tags')
    }
  }
}
</script>
