<template>
    <div>
        <h2>{{ form.id ? 'Editar Pessoa' : 'Nova Pessoa' }}</h2>

        <div class="nameField">
            <label>Nome</label>
            <input v-model="form.name" placeholder="Nome" />
        </div>

        <p v-if="error" class="error">O Nome é obrigatório.</p>

        <div class="emailField">
            <label>Email</label>
            <input v-model="form.email" placeholder="Email" />
        </div>

        <div class="phoneField">
            <label>Telefone</label>
            <input v-model="form.phone" placeholder="Telefone" />
        </div>

        <p v-if="error" class="error">O Telefone é obrigatório.</p>

        <div class="addressField">
            <label>Endereço</label>
            <input v-model="form.address" placeholder="Endereço" />
        </div>

        <div class="actionsBtn">
            <button @click="savePerson" class="button">
                {{ person ? 'Atualizar' : 'Salvar' }}
            </button>

            <button class="button" @click="cancel">
                Cancelar
            </button>
        </div>
    </div>
</template>

<script>
import api from '@/api/api'

export default {
    props: {
        person: {
            type: Object,
            default: null
        }
    },
    data() {
        return {
            form: {
                name: '',
                email: '',
                phone: '',
                address: ''
            },
            error: ''
        }
    },
    watch: {
        person: {
            immediate: true,
            handler(p) {
                if (p) {
                    this.form = { ...p };
                } else {
                    this.reset()
                }
            }
        }
    },
    methods: {
        async savePerson() {
            if (!this.form.name || !this.form.name.trim()) {
                this.error = 'O nome é obrigatório.'
                return
            } else {
                this.error = ''
            }

            if (!this.form.phone || !this.form.phone.trim()) {
                this.error = 'O telefone é obrigatório.'
                return
            } else {
                this.error = ''
            }

            if (this.person) {
                await api.put(`/persons/${this.person.id}`, this.form)
            } else {
                await api.post('/persons',
                    {
                        name: this.form.name,
                        email: this.form.email,
                        phone: this.form.phone,
                        address: this.form.address
                    })
            }

            this.$emit('saved')
            this.reset()
        },
        cancel() {
            this.reset()
            this.$emit('cancel')
        },
        reset() {
            this.form = {
                name: '',
                email: '',
                phone: '',
                address: ''
            }
            this.error = ''
        }
    }
}
</script>

<style>
.error {
    color: red;
    margin-top: 10px 0;
    font-size: 14px;
}

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

.actionsBtn {
    margin-top: 30px;
    display: flex;
    gap: 10px;
}

.actionsBtn .button {
    padding: 8px 14px;
    background-color: #2b2b2b;
    color: #ffffff;
    border: none;
    cursor: pointer;
}

.actionsBtn .button:hover {
    background-color: #00d0d0;
    color: #000;
}
</style>