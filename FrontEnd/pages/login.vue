<template>
  <v-container>
    <v-row justify="center" align="center" style="height: 80vh;">
      <v-col cols="12" sm="8" md="6" lg="4">
        <v-card>
          <v-card-title class="text-center">
            ورود به پنل مدیریت
          </v-card-title>
          <v-card-text>
            <v-form @submit.prevent="handleLogin">
              <v-text-field
                v-model="credentials.email"
                label="نام کاربری"
                required
                :disabled="isLoading"
              ></v-text-field>
              
              <v-text-field
                v-model="credentials.password"
                label="رمز عبور"
                type="password"
                required
                :disabled="isLoading"
              ></v-text-field>

              <v-alert
                v-if="error"
                type="error"
                class="mb-4"
              >
                {{ error }}
              </v-alert>

              <v-btn
                type="submit"
                color="primary"
                block
                :loading="isLoading"
              >
                ورود
              </v-btn>
            </v-form>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useApi } from '../useApi'

definePageMeta({
  middleware: ['auth']
})
const router = useRouter()
const api = useApi()

const credentials = ref({
  email: '',
  password: ''
})

const isLoading = ref(false)
const error = ref('')

const handleLogin = async () => {
  if (!credentials.value.email || !credentials.value.password) {
    error.value = 'لطفا ایمیل و رمز عبور را وارد کنید'
    return
  }

  isLoading.value = true
  error.value = ''

  try {
    const { data } = await api.login(credentials.value)
    
    if (data.value?.token) {
      localStorage.setItem('token', data.value.token)
      if (data.value.id) {
        localStorage.setItem('userId', data.value.id)
        localStorage.setItem('userEmail', data.value.email)
      }
      router.push('/admin')
    } else {
      error.value = 'خطا در ورود به سیستم'
    }
  } catch (err) {
    error.value = 'ایمیل یا رمز عبور اشتباه است'
  } finally {
    isLoading.value = false
  }
}
</script>
