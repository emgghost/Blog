<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <v-toolbar-title>وبلاگ یک حسابدار</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn to="/" text>خانه</v-btn>
    </v-app-bar>
    <v-main>
      <v-container>
        <NuxtPage /> <!-- 删除: <nuxt /> -->
      </v-container>
    </v-main>
  </v-app>

</template>
<style>
@import '@mdi/font/css/materialdesignicons.css';
</style>

