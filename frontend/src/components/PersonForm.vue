<template>
    <div>
        <h2>{{ form.id ? 'Editar Pessoa' : 'Nova Pessoa' }}</h2>

        <div class="nameField">
            <label>Nome</label>
            <input v-model="form.name" placeholder="Nome" />
        </div>

        <div class="emailField">
            <label>Email</label>
            <input v-model="form.email" placeholder="Email" />
        </div>

        <div class="phoneField">
            <label>Telefone</label>
            <input v-model="form.phone" placeholder="Telefone" />
        </div>

        <div class="addressField">
            <label>Endereço</label>
            <input v-model="form.address" placeholder="Endereço" />
        </div>

        <button @click="savePerson">Salvar</button>
    </div>
</template>

<script>
import api from '@/api/api'

export default {
    data() {
        return {
            form: {
                id: null,
                name: '',
                email: '',
                phone: '',
                address: ''
            }
        }
    },
    methods: {
        async savePerson() {
            if (this.form.id) {
                await api.put(`/persons/${this.form.id}`, this.form)
                alert('Pessoa atualizada com sucesso!')
            } else {
                await api.post('/persons', this.form)
                alert('Pessoa criada com sucesso!')
            }

            this.$emit('saved')
        }
    },
    props: {
        person: {
            type: Object,
            default: () => ({
                id: null,
                name: '',
                email: '',
                phone: '',
                address: ''
            })
        }
    },
    watch: {
        person: {
            immediate: true,
            handler(newVal) {
                if (newVal) {
                    this.form = { ...newVal };
                } else {
                    this.reset();
                }
            }
        }
    }
}
</script>

<style>
h2 {
    margin-bottom: 20px;
}

div.nameField {
    display: flex;
    flex-direction: column;
    max-width: 400px;
    gap: 5px;
}

div.emailField,
div.phoneField,
div.addressField,
div.passwordField {
    display: flex;
    flex-direction: column;
    max-width: 400px;
    margin-top: 20px;
    gap: 5px;
}

button {
    margin-top: 20px;
    padding: 8px 14px;
    background-color: #2b2b2b;
    color: #ffffff;
    border: none;
    cursor: pointer;
}

button:hover {
    background-color: #00d0d0;
    color: #000;
}
</style>