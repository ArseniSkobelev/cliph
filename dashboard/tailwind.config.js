/** @type {import('tailwindcss').Config} */

export default {
  content: ['./src/**/*.{html,js,svelte,ts}'],
  theme: {
    extend: {
      colors: {
        "theme-bg": "#fefefa",
        'theme-primary-accent': {
          '50': '#f2f9fc',
          '100': '#e8f4fc',
          '200': '#c8e2f7',
          '300': '#a9cff5',
          '400': '#6c9ceb',
          '500': '#3565e2',
          '600': '#2b56cc',
          '700': '#1d40ab',
          '800': '#132e87',
          '900': '#0a1d66',
          '950': '#051042'
        },
        'theme-secondary-accent': {
          '50': '#fffcf2',
          '100': '#fff8e6',
          '200': '#ffebbf',
          '300': '#ffdb99',
          '400': '#ffb54d',
          '500': '#ff8000',
          '600': '#e66f00',
          '700': '#bf5300',
          '800': '#993d00',
          '900': '#732a00',
          '950': '#4a1700'
        },
        'theme-danger': {
          '50': '#fcf7f0',
          '100': '#faefe1',
          '200': '#f2d6b6',
          '300': '#e8b88b',
          '400': '#d97941',
          '500': '#c83200',
          '600': '#b32a00',
          '700': '#942000',
          '800': '#781800',
          '900': '#591000',
          '950': '#3b0a00'
        },
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
  plugins: [
  ]
};