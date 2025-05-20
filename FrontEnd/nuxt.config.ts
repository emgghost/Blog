import { defineNuxtConfig } from 'nuxt/config'

export default defineNuxtConfig({
    target: 'static',
    ssr:false,
    routeRules: {
        '/': { prerender: true, ssr: true },
        '/admin/**': { prerender: false, ssr: false },
        '/auth/**': { prerender: true, ssr: true },
        '/posts/**': { prerender: true, ssr: true },
    },
    pages: true,

    app: {
        head: {
            titleTemplate: '%s',
            title: 'وبلاگ یک حسابدار',
            htmlAttrs: {
                lang: 'en'
            },
            meta: [
                { charset: 'utf-8' },
                { name: 'viewport', content: 'width=device-width, initial-scale=1' },
                { hid: 'description', name: 'description', content: '' },
                { name: 'format-detection', content: 'telephone=no' },

            ],
            link: [
                { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }
            ],
            htmlAttrs: {
                lang: 'fa-IR',
                dir: 'rtl'
            }
        }
    },

    css: ['~/assets/css/main.css', '~/assets/sass/main.scss'],

    plugins: [],

    components: true,

    buildModules: [
        '@nuxtjs/vuetify',
    ],

    modules: ['@nuxtjs/tailwindcss','nuxt-quasar-ui'],
    quasar: {
        animations: 'all',
        extras: ['fontawesome-v6'] as any,
        framework: {
            lang: 'fa',
            iconSet: 'fontawesome-v6'
        },
        plugins: ['Dialog', 'Notify'],
        lang: 'fa-IR',
        rtl: 'true',
        build: {
            rtl: true
        }
    },
    tailwindcss: {
        exposeConfig: true,
        viewer: true,
    },

    runtimeConfig: {
        public: {
            baseURL: 'http://localhost:5000/api'
        }
    },

    compatibilityDate: '2025-04-15'
})