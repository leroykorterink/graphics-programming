import StaticComponent from "../core/StaticComponent.js";

const getRandomClamped = (min = .8, max = 1) => Math.random() * (max - min) + min;

class Trees extends StaticComponent {
  constructor(minWidth = 4, maxWidth = 8, minHeight = 10, maxHeight = 12) {
    super();

    const width = Math.random() * (maxWidth - minWidth) + minWidth;
    const height = Math.random() * (maxHeight - minHeight) + minHeight;

    const material = new THREE.MeshNormalMaterial({ color: 0x00ff00 });
    const geometry = new THREE.CylinderGeometry(0, width, height, 15, 5);

    geometry.vertices.forEach(vertice => {
      vertice.x = vertice.x * getRandomClamped();
      vertice.y = vertice.y * getRandomClamped();
    });

    this.object = new THREE.Mesh(geometry, material);
  }

  
}

export default Trees;
