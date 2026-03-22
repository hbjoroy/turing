<script setup lang="ts">
import { ref } from 'vue';

// Reactive list of items
const items = ref<{ id: number; name: string; isEditing: boolean }[]>([]);

// Add a new item
const addItem = () => {
  const newItem = { id: Date.now(), name: '', isEditing: true };
  items.value.push(newItem);
};

// Remove an item
const removeItem = (id: number) => {
  items.value = items.value.filter(item => item.id !== id);
};

// Save the edited item
const saveItem = (item: { id: number; name: string; isEditing: boolean }) => {
  if (!item.name.trim()) {
    alert('Name cannot be empty.');
    return;
  }
  item.isEditing = false;
};

// Edit an item
const editItem = (item: { id: number; name: string; isEditing: boolean }) => {
  item.isEditing = true;
};
</script>

<template>
  <header>
    <div class="top">
      Turing Machine
    </div>
  </header>
  <main>
    <div class="container">
      <div class="content">
        <button @click="addItem" class="add-button">Legg til overgang</button>
        <ul class="item-list">
          <li v-for="item in items" :key="item.id" class="item">
            <div v-if="item.isEditing" class="edit-mode">
              <input
                v-model="item.name"
                type="text"
                placeholder="Enter name"
                class="name-input"
              />
              <button @click="saveItem(item)" class="save-button">Save</button>
            </div>
            <div v-else class="view-mode">
              <span class="item-name">{{ item.name }}</span>
              <button @click="editItem(item)" class="edit-button">Edit</button>
              <button @click="removeItem(item.id)" class="remove-button">X</button>
            </div>
          </li>
        </ul>
      </div>
    </div>
    <div class="footer">
      <div class="footer-content">
        <p>© 2025 Bjosoft. All rights reserved.</p>
      </div>
    </div>
  </main>
</template>

<style scoped lang="css">
.container {
  padding: 20px;
}

.add-button {
  margin-bottom: 10px;
  padding: 8px 12px;
  background-color: #4caf50;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.item-list {
  list-style: none;
  padding: 0;
}

.item {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
}

.edit-mode {
  display: flex;
  align-items: center;
}

.name-input {
  padding: 6px;
  margin-right: 8px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.save-button {
  padding: 6px 10px;
  background-color: #4caf50;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  margin-right: 8px;
}

.view-mode {
  display: flex;
  align-items: center;
}

.item-name {
  margin-right: 10px;
}

.edit-button {
  padding: 6px 10px;
  background-color: #2196f3;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  margin-right: 8px;
}

.remove-button {
  padding: 6px 10px;
  background-color: #f44336;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
</style>