<template>
    <div>
        <h2>Novo Usuário</h2>

        <div class="userField">
            <label>Nome de Usuário</label>
            <input v-model="user.userName" />
        </div>

        <div class="passwordField">
            <label>Senha</label>
            <input v-model="user.password" type="password" />
        </div>

        <button class="button" @click="saveUser">Salvar</button>
    </div>
</template>

<script>
import api from '@/api/api';

export default {
    data() {
        return {
            user: {
                userName: '',
                password: ''
            }
        }
    },
    methods: {
        async saveUser() {
            await api.post('/users', this.user);
            alert('Usuário criado com sucesso!');
            this.user.userName = '';
            this.user.password = '';
        }
    }
}
</script>

<style>
h2 {
    margin-bottom: 20px;
}

div.userField {
    display: flex;
    flex-direction: column;
    max-width: 400px;
    gap: 5px;
}

div.passwordField {
    display: flex;
    flex-direction: column;
    max-width: 400px;
    margin-top: 20px;
    gap: 5px;
}

input {
    background-color: #616161;
    border: none;
    color: #ffffff;
    padding: 7px;
    font-size: 14px;
}

.button {
    margin-top: 20px;
    padding: 8px 14px;
    background-color: #2b2b2b;
    color: #ffffff;
    border: none;
    cursor: pointer;
}

.button:hover {
    background-color: #00d0d0;
    color: #000;
}
</style>