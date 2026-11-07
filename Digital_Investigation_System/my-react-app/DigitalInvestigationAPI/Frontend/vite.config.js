
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
    base: '/Digital-Investigation-System/',

    plugins: [react()],

    server: {
        proxy: {
            '/api': {
                target: 'https://localhost:7031',
                changeOrigin: true,
                secure: false,
            },
        },
    },
})
