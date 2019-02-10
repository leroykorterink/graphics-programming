import StaticComponent from "../core/StaticComponent.js";

const getRandomClamped = (min = 0.7, max = 1) =>
  Math.random() * (max - min) + min;

const TreeFactory = (
  minWidth = 4,
  maxWidth = 6,
  minHeight = 10,
  maxHeight = 12
) =>
  class extends StaticComponent {
    constructor(scene) {
      super();

      const width = Math.random() * (maxWidth - minWidth) + minWidth;
      const height = Math.random() * (maxHeight - minHeight) + minHeight;

      const material = new THREE.MeshLambertMaterial({
        color: 0x1a6e24,
        emissive: 0x2d5521
      });
      const geometry = new THREE.CylinderGeometry(0, width, height, 15, 5);

      geometry.vertices.forEach(vertice => {
        vertice.x = vertice.x * getRandomClamped();
        vertice.y = vertice.y * getRandomClamped();
        vertice.z = vertice.z * getRandomClamped();
      });

      scene.add(new THREE.Mesh(geometry, material));
    }
  };

export default TreeFactory;
