export const state = () => ({
  blogs: []
});

export const mutations = {
  SET_BLOGS(state, blogs) {
    state.blogs = blogs;
  }
};

export const actions = {
  async getBlogs({ commit }) {
    try {
      const response = await this.$axios.get('/blogs');
      commit('SET_BLOGS', response.data);
      return response.data;
    } catch (error) {
      console.error('Failed to fetch blogs:', error);
    }
  },
  async getBlogsByTag({ commit }, tag) {
    try {
      const response = await this.$axios.get(`/blogs?tag=${tag}`);
      commit('SET_BLOGS', response.data);
      return response.data;
    } catch (error) {
      console.error('Failed to fetch blogs by tag:', error);
    }
  }
};

export const getters = {
  blogs: state => state.blogs
};