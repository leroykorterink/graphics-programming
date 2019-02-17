import StaticComponent from "../core/StaticComponent.js";
import makeFBXLoader from "../util/makeFBXLoader.js";

const loadFBX = makeFBXLoader(fileName => `assets/Rooster/${fileName}`);

export default () =>
  class Rooster extends StaticComponent {
    constructor(scene) {
      super();

      this.init(scene);
    }

    async init(scene) {
      const group = await loadFBX("rooster_model.fbx");
      const mesh = group.children[0];

      mesh.material = new THREE.MeshStandardMaterial({
        color: "#E38640",
        roughness: 1,
        metalness: 0.1,
        flatShading: true
      });

      mesh.translateZ(-100);

      scene.add(mesh);
    }
  };
