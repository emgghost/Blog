export const useApi = () => {
  const config = useRuntimeConfig();
  const fileUrl = config.public.fileUrl || 'http://localhost:5000'; // 替换为实际的文件URL前缀
  const baseURL = config.public.baseURL || 'http://localhost:5000/api';

  // Get token from local storage or another source
  const token = process.client ? localStorage.getItem('token') : null;

  const request = (endpoint, options = {}) => {
    return useAsyncData(endpoint, () =>
      $fetch(`${baseURL}${endpoint}`, {
        ...options,
        method: options.method || 'GET',
        headers: {
          Authorization: token ? `Bearer ${token}` : '',
          ...(options.headers || {}),
        },
      })
    );
  };

  // Auth API
  const login = (credentials) => {
    return request('/auth/login', { method: 'POST', body: credentials });
  };
  const register = (userData) => {
    return request('/auth/register', { method: 'POST', body: userData });
  };
  const refreshToken = (token) => {
    return request('/auth/refresh-token', { method: 'POST', body: { token } });
  };

  // Blog Posts API
  const getPosts = () => {
    return request('/posts');
  };
  const getPostBySlug = (slug) => {
    return request(`/posts/${slug}`,);
  };
  const createPost = (postData) => {
    return request('/posts', { method: 'POST', body: postData });
  };
  const updatePost = (id, postData) => {
    return request(`/posts/${id}`, { method: 'PUT', body: postData });
  };
  const deletePost = (id) => {
    return request(`/posts/${id}`, { method: 'DELETE' });
  };

  // Categories API
  const getCategories = () => {
    return request('/categories');
  };
  const createCategory = (categoryData) => {
    return request('/categories', { method: 'POST', body: categoryData });
  };
  const updateCategory = (id, categoryData) => {
    return request(`/categories/${id}`, { method: 'PUT', body: categoryData });
  };
  const getCategory = (id) => {
    return request(`/categories/${id}`, { method: 'GET' });
  };
  const deleteCategory = (id) => {
    return request(`/categories/${id}`, { method: 'DELETE' });
  };

  // Tags API
  const getTags = () => {
    return request('/tags');
  };
  const createTag = (tagData) => {
    return request('/tags', { method: 'POST', body: tagData });
  };
  const deleteTag = (id) => {
    return request(`/tags/${id}`, { method: 'DELETE' });
  };

  const getPostByTag = (slug) => {
    return request(`/tags/${slug}`, { method: 'GET' });
  };
  const getTag = (id) => {
    return request(`/tags/${id}`, { method: 'GET' });
  }

  const updateTag = (id, tagData) => {
    return request(`/tags/${id}`, { method: 'PUT', body: tagData });
  };

  // Comments API
  const getPostComments = (postId) => {
    return request(`/posts/${postId}/comments`);
  };
  const createComment = (commentData) => {
    return request('/comments', { method: 'POST', body: commentData });
  };
  const deleteComment = (id) => {
    return request(`/comments/${id}`, { method: 'DELETE' });
  };

  // User Management API
  const getUsers = () => {
    return request('/users');
  };
  const updateUserRole = (userId, roleData) => {
    return request(`/users/${userId}/role`, { method: 'PUT', body: roleData });
  };

  // File Management API
  const uploadFile = (file) => {
    return $fetch(`${baseURL}/FileManager/upload`, {
      method: 'POST',
      body: file,
      headers: {
        Authorization: token ? `Bearer ${token}` : '',
      }
    })
  };
  const deleteFile = (fileName) => {
    return request(`/files/${fileName}`, { method: 'DELETE' });
  }

  return { 
    request, 
    fileUrl, 
    login, 
    register, 
    refreshToken, 
    getPosts,
    getPostBySlug,
    createPost, 
    updatePost, 
    deletePost, 
    uploadFile, 
    deleteFile,
    getCategories,
    createCategory,
    updateCategory,
    deleteCategory,
    getTags,
    createTag,
    deleteTag,
    getPostComments,
    createComment,
    deleteComment,
    getUsers,
    updateUserRole,
    getCategory,
    getTag,
    updateTag,
    getPostByTag
  };
};
