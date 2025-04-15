/** @type {import('tailwindcss').Config} */
module.exports = {
  important: true, // ⬅️ Ensures Tailwind has priority over Vuetify
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {},
  },
  plugins: [],
};