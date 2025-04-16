import lang from "quasar/lang/fa-IR.js"
import iconSet from "quasar/icon-set/material-icons.js"
import { Dialog,Notify } from "quasar"


export const componentsWithDefaults = {  }

export const appConfigKey = "nuxtQuasar"

export const quasarNuxtConfig = {
  lang,
  iconSet,
  components: {"defaults":{}},
  plugins: {Dialog,Notify},
  
}