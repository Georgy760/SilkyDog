//using System;
//using UnityEngine;
//using UnityEngine.UI;

//namespace Common.Scripts.Legacy
//{
//    public class DogControl : MonoBehaviour
//    {
//        [SerializeField] internal float speed = 7;
//        [SerializeField] internal float objects_speed = 5;

//        [SerializeField] private float jump_speed = 1000;

//        [SerializeField] internal Text coins_text;
//        [SerializeField] private Text metres_text;
//        [SerializeField] internal GameObject panel_play;
//        [SerializeField] internal GameObject panel_lose;

//        internal int coins = 0;

//        private Rigidbody2D rb;
//        private bool is_grounded;
//        private int coll_counts = 0;
//        private double distance;
//        private Generator generator;

//        private void Start()
//        {
//            coins = 0;
//            distance = 0;
//            is_grounded = true;

//            rb = this.gameObject.GetComponent<Rigidbody2D>();
//            generator = GameObject.FindObjectsOfType<Generator>()[0];
//            EventManager.eventOnCoinsCollect = null;
//            EventManager.eventOnCoinsCollect += (int coins_num) => coins_text.text = coins_num.ToString();
//        }

//        private void FixedUpdate()
//        {
//            distance += 0.1;
//            if (distance % 200 == 0)
//                generator.ChangeLocation();
//            distance = Math.Round(distance, 2);
//        }

//        private void Update()
//        {
//            metres_text.text = "Metres: " + distance.ToString();
//            this.transform.rotation = new Quaternion(0, 0, 0, 1);

//            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && is_grounded && coll_counts > 0) {
//                is_grounded = false;
//                rb.AddForce(transform.up * jump_speed * 100);
//                AudioManager.instance.PlayOneShot(AudioManager.instance.GetSound("dog_jump"), SoundType.Effects);
//            }
//            else if (Input.GetKey(KeyCode.A)) {
//                this.gameObject.transform.Translate(speed * Time.deltaTime * -1, 0, 0);
//            }
//            else if (Input.GetKey(KeyCode.D)) {
//                this.gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
//            }
//            if (this.gameObject.transform.position.x < -8.4f) {
//                this.gameObject.transform.position = new Vector3(-8.4f, this.gameObject.transform.position.y, 0);
//            }
//            else if (this.gameObject.transform.position.x > 8.4f) {
//                this.gameObject.transform.position = new Vector3(8.4f, this.gameObject.transform.position.y, 0);
//            }
//        }
//        private void OnCollisionEnter2D(Collision2D col)
//        {
//            coll_counts++;
//            if (col.gameObject.layer == 6)
//                is_grounded = true;
//        }

//        private void OnCollisionExit2D(Collision2D col)
//        {
//            coll_counts--;
//        }

//        public void SetPause(bool pause)
//        {
//            Time.timeScale = pause ? 0 : 1;
//        }
//    }
//}