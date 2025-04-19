export default defineNuxtRouteMiddleware((to) => {
  if (process.server) return // don't redirect on server side
  
  const token = localStorage.getItem('token')
  
  // If user is not authenticated and trying to access admin pages
  if (!token && to.path.startsWith('/admin')) {
    return navigateTo('/login')
  }
  if (token && to.path.startsWith('/login')) {
    console.log('triggered');
    return navigateTo('/admin')
  }
})
