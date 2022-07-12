using UnityEngine;
using System.Collections;

namespace com.tictactoe.game
{
	public class SoundManager : MonoBehaviour
	{
		[SerializeField] private SFXMapper sfxMapper;
		[SerializeField] private AudioSource sfxSource;
		[SerializeField] private float sfxVolume = 1.0f;

		public void PlayTileClickSound()
		{
			PlaySfx(sfxMapper.tileClickSound);
		}

		public void PlayHumanWinSound()
		{
			PlaySfx(sfxMapper.humanWinGameSound);
		}

		public void PlayHumanLoseSound()
		{
			PlaySfx(sfxMapper.humanLoseGameSound);
		}

		public void PlayStartGameSound()
		{
			PlaySfx(sfxMapper.startGameSound);
		}

		public void PlayGameTieSound()
		{
			PlaySfx(sfxMapper.gameTieGameSound);
		}

		public void DisableSound()
		{
			sfxSource.Stop();
			sfxSource.enabled = false;
		}

		public void SetSfxVolume()
		{
			sfxSource.volume = sfxVolume;
		}

		public void FadeOutSfx(float time = 1.0f)
		{
			StartCoroutine(CO_FadeOut(sfxSource, time));
		}

		public void FadeInSfx(float time = 1.0f)
		{
			StartCoroutine(CO_FadeIn(sfxSource, time));
		}

		public void PlaySfx(AudioClip clip)
		{
			if (clip)
			{
				SetSfxVolume();
				if (!sfxSource.isPlaying)
				{
					sfxSource.clip = clip;
					sfxSource.Play();
				}
				else
				{
					StartCoroutine(CO_FadeOutIn(clip));
				}
			}
		}

		IEnumerator CO_FadeOutIn(AudioClip clip)
		{
			yield return CO_FadeOut(sfxSource, 0.1f);
			PlaySfx(clip);
		}

		public float PlaySfx(AudioClip clip, float vol = 1f)
		{
			if (clip)
			{
				AudioSource source = sfxSource;

				source.clip = clip;
				source.volume = vol;

				source.Play();
				return source.clip.length;

			}
			return 0f;
		}


		IEnumerator CO_FadeOut(AudioSource audioSource, float time)
		{
			float startVolume = audioSource.volume;
			while (audioSource.volume > 0)
			{
				audioSource.volume -= startVolume * Time.deltaTime / time;
				yield return null;
			}
			audioSource.Stop();
		}

		IEnumerator CO_FadeIn(AudioSource audioSource, float time)
		{
			audioSource.Play();
			audioSource.volume = 0f;
			while (audioSource.volume < 1)
			{
				audioSource.volume += Time.deltaTime / time;
				yield return null;
			}
		}
	}
}
