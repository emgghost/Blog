<!-- pages/admin/posts/index.vue -->
<template>
  <v-container class="border rounded-md">
    <v-btn to="/admin/posts/create" color="success">
      <v-icon icon="mdi-plus"/>
      ایجاد پست جدید
    </v-btn>
    <v-table>
      <thead>
        <tr>
          <th>عنوان</th>
          <th>نویسنده</th>
          <th>تاریخ ایجاد</th>
          <th>عملیات</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="post in posts" :key="post.id">
          <td>{{ post.title }}</td>
          <td>{{ post.author?.displayName || 'ناشناس' }}</td>
          <td>{{ new Date(post.createdAt).toLocaleDateString('fa-IR') }}</td>
          <td>
            <v-btn :to="`/admin/posts/edit?slug=${post.slug}`" icon class="ml-1">
              <v-icon icon="mdi-pencil" color="blue"/>
            </v-btn>
            <v-btn @click="deletePost(post.id)" icon>
              <v-icon icon="mdi-delete" color="red"/>
            </v-btn>
          </td>
        </tr>
      </tbody>
    </v-table>
  </v-container>
</template>

<script setup>
import { ref } from "vue";
import { useApi } from "../../../useApi";

definePageMeta({
  layout: 'admin',
  middleware: ['auth']
})

const api = useApi();
const posts = ref([]);

// Fetch posts
const fetchPosts = async () => {
  const { data } = await api.getPosts();
  posts.value = data.value;
};

// Fetch posts on component mount
await fetchPosts();

// Delete post
const deletePost = async (id) => {
  try {
    await api.deletePost(id);
    await fetchPosts(); // Refresh data after deletion
  } catch (error) {
    console.error("Error deleting post:", error);
  }
};
</script>
