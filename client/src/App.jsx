import { useState, useEffect } from 'react'
import { getItems, createItem, deleteItem } from './api'

function App() {
  const [items, setItems] = useState([])
  const [name, setName] = useState('')

  // 1. Load data from Backend when app starts
  useEffect(() => {
    fetchItems()
  }, [])

  const fetchItems = async () => {
    try {
      const response = await getItems()
      setItems(response.data)
    } catch (error) {
      console.error("Error fetching data:", error)
      alert("Backend not reachable! Is it running?")
    }
  }

  // 2. Add a new item to MongoDB
  const handleAdd = async () => {
    if (!name) return
    const newItem = { name: name, category: "Test" }
    await createItem(newItem)
    setName('')      // Clear input
    fetchItems()     // Refresh list
  }

  // 3. Delete an item from MongoDB
  const handleDelete = async (id) => {
    await deleteItem(id)
    fetchItems()     // Refresh list
  }

  return (
    <div style={{ padding: '20px', fontFamily: 'sans-serif', maxWidth: '600px', margin: '0 auto' }}>
      <h1>Azure Fullstack POC</h1>
      
      {/* Input Section */}
      <div style={{ marginBottom: '20px', display: 'flex', gap: '10px' }}>
        <input 
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Enter item name..."
          style={{ padding: '10px', flexGrow: 1 }}
        />
        <button onClick={handleAdd} style={{ padding: '10px 20px', cursor: 'pointer' }}>
          Add to Database
        </button>
      </div>

      {/* List Section */}
      <ul style={{ listStyle: 'none', padding: 0 }}>
        {items.map((item) => (
          <li key={item.id} style={{ 
            background: '#f4f4f4', 
            margin: '5px 0', 
            padding: '10px', 
            display: 'flex', 
            justifyContent: 'space-between' 
          }}>
            <span>{item.name}</span>
            <button onClick={() => handleDelete(item.id)} style={{ color: 'red', cursor: 'pointer' }}>
              Delete
            </button>
          </li>
        ))}
      </ul>
      
      {items.length === 0 && <p>No items found. Add one!</p>}
    </div>
  )
}

export default App