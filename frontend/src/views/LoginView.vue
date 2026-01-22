<template>
    <div class="login-view">
        <h2>Login</h2>

        <div class="fields">
            <input v-model="username" placeholder="Usuário" />
            <input v-model="password" type="password" placeholder="Senha" />
        </div>

        <button class="button-login" @click="login">Entrar</button>

        <p v-if="error" style="color:red">{{ error }}</p>
    </div>
</template>

<script>
import api from '@/api/api'

export default {
    data() {
        return {
            username: '',
            password: '',
            error: ''
        }
    },
    methods: {
        async login() {
            try {
                const res = await api.post('/auth/login', {
                    username: this.username,
                    password: this.password
                })

                sessionStorage.setItem('token', res.data.token)
                this.$emit('logged-in')
            } catch {
                this.error = 'Usuário ou senha inválidos'
            }
        }
    }
}
</script>

<style>
.login-view {
    display: flex;
    flex-direction: column;
    align-items: center;
    max-width: 300px;
    margin: 10px auto;
    border: none;
}

.fields {
    display: flex;
    flex-direction: column;
    gap: 10px;
    margin-bottom: 15px;
}

.button-login {
    padding: 8px 14px;
    background-color: #2b2b2b;
    color: #ffffff;
    border: none;
    cursor: pointer;
}

.button-login:hover {
    background-color: #00d0d0;
    color: #000;
}
</style>