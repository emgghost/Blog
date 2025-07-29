import { defineNuxtConfig } from 'nuxt/config'
import {baseURL} from "nuxt/dist/core/runtime/nitro/paths";
const createSitemapRoutes = async () => {
    let routes = [{ url: '/', priority: 1.0,changefreq:'weekly' },{url:'/posts', priority:0.9,changefreq:'weekly'}];
    const data = await fetch('https://blogapi.yekhesabdar.com/api/posts')
    const posts = await data.json();
    for (const post of posts) {
        routes.push(`posts/${post.slug}`);
    }
    return routes;
}
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

    modules: ['@nuxtjs/tailwindcss', 'nuxt-quasar-ui', '@nuxtjs/sitemap'],
    sitemap: {
        hostname: 'https://blog.yekhesabdar.com',
        xsl: false,
        gzip: true,
        defaults: {
            lastmod: '2025-07-22T14:32:35+00:00',
            priority: 0.8,
            changefreq: 'monthly'
        },
        exclude: [
            '/admin/**',
            '/login/**',
            '/categories/**',
            '/auth/**',
            '/inspire',
            '/tags/**',
        ],
        urls: createSitemapRoutes
        },

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
            baseURL: 'https://blogapi.yekhesabdar.com/api',
            fileUrl: 'https://blogapi.yekhesabdar.com'
        }
    },

    compatibilityDate: '2025-04-15'
})