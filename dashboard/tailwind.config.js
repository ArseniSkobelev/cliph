/** @type {import('tailwindcss').Config} */
export default {
  content: ['./src/**/*.{html,js,svelte,ts}'],
  theme: {
    extend: {
      colors: {
        "theme-bg": "#fefefa",
        "theme-primary-accent": "#516279",
        "theme-primary-accent-darker": "#333e4d",
        "theme-light-text": "#fefefa",
      },
      boxShadow: {
        "elevation": "4px 0px 4px 0px rgba(0,0,0,0.1)"
      },
      fontFamily : {
        'sans': ['Inter', 'sans-serif']
      }
    }
  },
  plugins: []
};