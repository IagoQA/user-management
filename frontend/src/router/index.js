import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '@/views/LoginView.vue'
import PersonsView from '@/views/PersonsView.vue'

const routes = [
    {
        path: '/login',
        component: LoginView
    },
    {
        path: '/persons',
        component: PersonsView,
        meta: { requiresAuth: true }
    },
    {
        path: '/:pathMatch(.*)*',
        redirect: '/persons'
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

router.beforeEach((to, next) => {
    const token = localStorage.getItem('token')

    if (to.meta.requiresAuth && !token) {
        next('/login')
    } else {
        next()
    }
})

export default router