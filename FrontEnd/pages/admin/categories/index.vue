<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <h1>مدیریت دسته‌بندی‌ها</h1>
      </v-col>
    </v-row>

    <!-- Add Category Form -->
    <v-row>
      <v-col cols="12" md="6">
        <v-card>
          <v-card-title>
            {{ editingCategory ? 'ویرایش دسته‌بندی' : 'افزودن دسته‌بندی جدید' }}
          </v-card-title>
          <v-card-text>
            <v-form @submit.prevent="handleSubmit">
              <v-text-field
                v-model="newCategory.name"
                label="نام دسته‌بندی"
                required
                :loading="isSubmitting"
              ></v-text-field>
              <v-btn 
                type="submit" 
                color="primary" 
                :loading="isSubmitting"
              >
                {{ editingCategory ? 'ذخیره تغییرات' : 'افزودن' }}
              </v-btn>
              <v-btn 
                v-if="editingCategory"
                @click="cancelEdit"
                class="mr-2"
              >
                انصراف
              </v-btn>
            </v-form>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <!-- Categories List -->
    <v-row class="mt-4">
      <v-col cols="12">
        <v-card>
          <v-card-title>لیست دسته‌بندی‌ها</v-card-title>
          <v-card-text>
            <v-table>
              <thead>
                <tr>
                  <th>نام</th>
                  <th>اسلاگ</th>
                  <th>عملیات</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="category in categories" :key="category.id">
                  <td>{{ category.name }}</td>
                  <td>{{ category.slug }}</td>
                  <td>
                    <v-btn
                      icon="mdi-pencil"
                      size="small"
                      color="primary"
                      class="mr-2"
                      @click="editCategory(category)"
                    >
                    </v-btn>
                    <v-btn
                      icon="mdi-delete"
                      size="small"
                      color="error"
                      @click="deleteCategory(category.id)"
                      :loading="deletingId === category.id"
                    >
                    </v-btn>
                  </td>
                </tr>
              </tbody>
            </v-table>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useApi } from '../../../useApi'

definePageMeta({
  layout: 'admin',
  middleware: ['auth']
})

const api = useApi()
const categories = ref([])
const isSubmitting = ref(false)
const deletingId = ref(null)
const editingCategory = ref(null)
const newCategory = ref({
  name: '',
  slug: ''
})

const fetchCategories = async () => {
  try {
    const { data } = await api.getCategories()
    categories.value = data.value
  } catch (error) {
    console.error('Error fetching categories:', error)
  }
}

const handleSubmit = async () => {
  if (!newCategory.value.name) return

  isSubmitting.value = true
  try {
    if (editingCategory.value) {
      await api.updateCategory(editingCategory.value.id, newCategory.value)
    } else {
      await api.createCategory(newCategory.value)
    }
    await fetchCategories()
    resetForm()
  } catch (error) {
    console.error('Error saving category:', error)
  } finally {
    isSubmitting.value = false
  }
}

const deleteCategory = async (id) => {
  if (!confirm('آیا از حذف این دسته‌بندی اطمینان دارید؟')) return

  deletingId.value = id
  try {
    await api.deleteCategory(id)
    await fetchCategories()
  } catch (error) {
    console.error('Error deleting category:', error)
  } finally {
    deletingId.value = null
  }
}

const editCategory = (category) => {
  editingCategory.value = category
  newCategory.value = {
    name: category.name,
    slug: category.slug
  }
}

const cancelEdit = () => {
  editingCategory.value = null
  resetForm()
}

const resetForm = () => {
  newCategory.value = {
    name: '',
    slug: ''
  }
  editingCategory.value = null
}

onMounted(fetchCategories)
</script>
