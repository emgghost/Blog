<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" sm="8" md="6">
        <v-card>
          <v-card-title>Edit Tag</v-card-title>
          <v-card-text>
            <v-form @submit.prevent="submitForm">
              <v-text-field v-model="tag.name" label="Title" required></v-text-field>
              <v-textarea v-model="tag.description" label="Description"></v-textarea>
              <v-btn type="submit" color="primary">Save</v-btn>
            </v-form>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      tag: {
        id: '', // Ensure id is included in the data object
        name: '',
        description: ''
      }
    };
  },
  created() {
    this.fetchTag();
  },
  methods: {
    async fetchTag() {
      // Assume we get the tag ID from query parameters
      const tagId = this.$route.query.id;
      try {
        const response = await this.$axios.get(`/tags/${tagId}`);
        this.tag = response.data; // This should include the id
      } catch (error) {
        console.error('Failed to fetch tag:', error);
      }
    },
    async submitForm() {
      try {
        const response = await this.$axios.put(`/tags/${this.tag.id}`, this.tag); // Ensure tag.id is used here
        console.log('Tag updated successfully:', response.data);
        this.$router.push('/tags');

        // You can add redirection logic here, such as redirecting to the tags list page
      } catch (error) {
        console.error('Failed to update tag:', error);
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
