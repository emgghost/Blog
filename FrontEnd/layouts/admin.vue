<template>
  <v-app>
    <AdminNavDrawer v-model="drawer" />
    <AdminHeader @toggle-drawer="drawer = !drawer" />
    
    <v-main>
      <v-container fluid>
        <slot />
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup>
import { ref } from 'vue'
import AdminNavDrawer from '@/components/admin/AdminNavDrawer.vue'
import AdminHeader from '@/components/admin/AdminHeader.vue'

definePageMeta({
  middleware: 'auth'
})

const drawer = ref(true)
</script>

<style>
@import '@mdi/font/css/materialdesignicons.css';
</style>