<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <h1>مدیریت برچسب‌ها</h1>
      </v-col>
    </v-row>

    <!-- Add Tag Form -->
    <v-row>
      <v-col cols="12" md="6">
        <v-card>
          <v-card-title>
            {{ editingTag ? 'ویرایش برچسب' : 'افزودن برچسب جدید' }}
          </v-card-title>
          <v-card-text>
            <v-form @submit.prevent="handleSubmit">
              <v-text-field
                v-model="newTag.name"
                label="نام برچسب"
                :required="editingTag ? true : false"
                :loading="isSubmitting"
              ></v-text-field>
              <v-btn 
                type="submit" 
                color="primary"
                :loading="isSubmitting"
              >
                {{ editingTag ? 'ذخیره تغییرات' : 'افزودن' }}
              </v-btn>
              <v-btn 
                v-if="editingTag"
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
    <!-- Tags List -->
    <v-row class="mt-4">
      <v-col cols="12">
        <v-card class="shadow-2xl">
          <v-card-title>لیست برچسب‌ها</v-card-title>
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
                <tr v-for="tag in tags" :key="tag.id">
                  <td>{{ tag.name }}</td>
                  <td>{{ tag.slug }}</td>
                  <td>
                    <v-btn
                      icon="mdi-pencil"
                      size="small"
                      color="primary"
                      class="ml-2"
                      @click="editTag(tag)"
                    >
                    </v-btn>
                    <v-btn
                      icon="mdi-delete"
                      size="small"
                      color="error"
                      @click="deleteTag(tag.id)"
                      :loading="deletingId === tag.id"
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
const tags = ref([])
const isSubmitting = ref(false)
const deletingId = ref(null)
const editingTag = ref(null)
const newTag = ref({
  name: '',
  slug: ''
})

const fetchTags = async () => {
  try {
    const { data } = await api.getTags()
    tags.value = data.value
  } catch (error) {
    console.error('Error fetching tags:', error)
  }
}

const handleSubmit = async () => {
  console.log("dasdasda");
  
  if (!newTag.value.name) return

  isSubmitting.value = true
  try {
    if (editingTag.value) {
      await api.updateTag(editingTag.value.id, newTag.value)
    } else {
      await api.createTag(newTag.value)
    }
    await fetchTags()
    resetForm()
  } catch (error) {
    console.error('Error saving tag:', error)
  } finally {
    isSubmitting.value = false
  }
}

const deleteTag = async (id) => {
  if (!confirm('آیا از حذف این برچسب اطمینان دارید؟')) return

  deletingId.value = id
  try {
    await api.deleteTag(id)
    await fetchTags()
  } catch (error) {
    console.error('Error deleting tag:', error)
  } finally {
    deletingId.value = null
  }
}

const editTag = (tag) => {
  editingTag.value = tag
  newTag.value = {
    name: tag.name,
    slug: tag.slug
  }
}

const cancelEdit = () => {
  editingTag.value = null
  resetForm()
}

const resetForm = () => {
  newTag.value = {
    name: '',
    slug: ''
  }
  editingTag.value = null
}

onMounted(fetchTags)
</script>
