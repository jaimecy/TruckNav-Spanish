<script lang="ts" setup>
const props = defineProps<{
    type: "note" | "warning" | "caution";
}>();

const { t } = useTranslations();

const title = computed(() => {
    if (props.type === "note") return t("common.note");
    if (props.type === "warning") return t("common.warning");
    return t("common.caution");
});
</script>

<template>
    <div class="info-box">
        <div class="title">
            <Icon name="lucide:info" class="icon" />
            <p>{{ title }}</p>
        </div>

        <slot name="content"></slot>
    </div>
</template>

<style
    lang="scss"
    scoped
    src="~/assets/scss/scoped/common/infoBox.scss"
></style>
