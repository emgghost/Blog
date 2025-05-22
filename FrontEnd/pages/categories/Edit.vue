<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" sm="8" md="6">
        <v-card>
          <v-card-title>Edit Category</v-card-title>
          <v-card-text>
            <v-form @submit.prevent="submitForm">
              <v-text-field v-model="category.name" label="Title" required></v-text-field>
              <v-textarea v-model="category.description" label="Description"></v-textarea>
              <v-btn type="submit" color="primary">Save</v-btn>
            </v-form>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { useApi } from '../../useApi';
const api = useApi();

export default {

  data() {
    return {
      category: {
        id: '', // Ensure id is included in the data object
        name: '',
        description: ''
      }
    };
  },
  created() {
    this.fetchCategory();
  },

  methods: {
    async fetchCategory() {

      // Assume we get the category ID from query parameters
      const categoryId = this.$route.query.id;
      try {
        this.category = (api.getCategory(categoryId)).data
      } catch (error) {
        console.error('Failed to fetch category:', error);
      }
    },
    async submitForm() {
      try {
        const response = await api.updateCategory(this.category.id, this.category); // Ensure category.id is used here
        console.log('Category updated successfully:', response.data);
        // You can add redirection logic here, such as redirecting to the categories list page
      } catch (error) {
        console.error('Failed to update category:', error);
      }
    }
  }
};
</script>

<style scoped>
/* Add some styles */
form {
  max-width: 400px;
  margin: 0 auto;
}
div {
  margin-bottom: 10px;
}
label {
  display: block;
  margin-bottom: 5px;
}
input, textarea {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
}
button {
  padding: 10px 15px;
  background-color: #007bff;
  color: white;
  border: none;
  cursor: pointer;
}
button:hover {
  background-color: #0056b3;
}
</style>